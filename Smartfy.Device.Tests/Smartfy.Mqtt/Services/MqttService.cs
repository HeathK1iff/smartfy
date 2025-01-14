using Microsoft.Extensions.Logging;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;
using Smartfy.Core.Utils;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Mqtt.Exceptions;
using Smartfy.Mqtt.Entity;
using Smartfy.Mqtt.Configuration.Impl;
using Smartfy.Mqtt.Configuration;

namespace Smartfy.Mqtt.Services
{
    internal class MqttService: IMqttService
    {
        private readonly MqttClient _client;
        private IMqttConfiguration _configuration;
        private ILogger? _logger;
        private IMessageService _messageService;

        public MqttService(IMqttConfiguration configuration,
            ILogger logger, IMessageService messageService)
        {
            _messageService = messageService;

            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            if (_messageService is null)
                throw new ArgumentNullException(nameof(_messageService));

            _logger = logger;

            _configuration = configuration;

            if (string.IsNullOrWhiteSpace(_configuration.Host))
                throw new HostNotFoundException($"Host ({_configuration.Host}) is not found");

            _client = new MqttClient(_configuration.Host);
            _client.MqttMsgPublishReceived += MqttMsgReceived;
        }

        private void MqttMsgReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string messageData = System.Text.Encoding.Default.GetString(e.Message);
            _logger?.LogDebug($"Message is received from {e.Topic}");

            _messageService.Publish(new MqttMessage()
            {
                Topic = e.Topic,
                Data = messageData
            });
        }

        public void Start()
        {
            if (_client.IsConnected)
            {
                _logger?.LogWarning($"Client({_configuration.Host}-{_configuration.ClientId}) is connected already");
                return;
            }

            if (string.IsNullOrWhiteSpace(_configuration.ClientId))
            {
                _configuration.ClientId = Guid.NewGuid().ToString();
                _logger?.LogWarning($"Client Id is not defined. Generated new client Id");
            }

            _client.Subscribe(new string[] { _configuration.SubsribeTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            try
            {
                _client.Connect(_configuration.ClientId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Connection is not established");
            }
        }

        public void Stop()
        {
            try
            {
                _client.Disconnect();
                _logger?.LogWarning($"Listening of {_configuration.Host} was stopped");
            } catch (Exception ex)
            {
                _logger?.LogError(ex, "Disconnect error");
            }
            
        }
    }
}
