using Microsoft.Extensions.Logging;
using Smartfy.Core.Entities;
using Smartfy.Core.Messages;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Core.Utils;
using Smartfy.TelegramBot.Configuration;
using Smartfy.TelegramBot.Exceptions;
using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Smartfy.TelegramBot.Services
{
    internal class TelegramService : ITelegramService, IMessageSubscriber
    {
        private TelegramBotClient _client;
        private CancellationTokenSource _ctsSendMessage = new();
        private CancellationTokenSource _ctsReceiveMessage = new();
        private ITelegramConfiguration _configuration;
        private ILogger? _logger;
        private readonly IMessageService _messageService;

        public TelegramService(ITelegramConfiguration configuration,
            ILogger logger, IMessageService messageService)
        {
            _messageService = messageService;

            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            if (_messageService is null)
                throw new ArgumentNullException(nameof(_messageService));

            _configuration = configuration;

            _logger = logger;

            _messageService.Subscribe<OutputMessage>(this);

            if (_configuration.Token is null)
                throw new TokenNotDefinedException("Telegram token is not defined");

            _client = new TelegramBotClient(_configuration.Token);
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _client.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: _ctsReceiveMessage.Token
            );

        }

        private Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return Task.CompletedTask;
            if (message.Text is not { } messageText)
                return Task.CompletedTask;
            if (message.From?.Username is null)
                return Task.CompletedTask;

            if (!_configuration.Sessions.TryGetChatId(message.From.Username, out long? chatId))
            {
                _configuration.Sessions.Add(message.From.Username, message.Chat.Id);
                _logger?.LogInformation($"New client ({message.From.Username}:{message.From.LastName}:{message.From.FirstName}) was registered");
                _configuration.Save();
            }

            _messageService.Publish(new IncomeMessage()
            {
                Sender = message.From.Username,
                Data = messageText
            });

            _logger?.LogDebug($"Data message was received ({message.From.Username}:{messageText.Length} bytes)");

            return Task.CompletedTask;
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void OnReceived(Core.Entities.Message message)
        {
            var outMessage = message as OutputMessage;

            if (outMessage is null)
                return;

            if (_configuration.Sessions.TryGetChatId(outMessage.Recepient, out long? chatId))
            {
                _client.SendTextMessageAsync(
                    chatId: chatId,
                    text: message.Data,
                    cancellationToken: _ctsSendMessage.Token);
                _logger?.LogDebug($"Data message was send ({outMessage.Recepient}:{message.Data.Length} bytes)");
                return;
            }

            _logger?.LogWarning($"Corresponding session is not found for ({outMessage.Recepient}) recepient");
        }
    }
}