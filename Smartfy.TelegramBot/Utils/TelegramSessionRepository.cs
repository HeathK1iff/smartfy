using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Smartfy.TelegramBot.Classes
{
    public class TelegramSessionRepository : IDisposable, ITelegramSessionRepository
    {
        private readonly FileStream _stream;
        private readonly ILogger _logger;

        public TelegramSessionRepository(FileStream stream, ILogger logger)
        {
            _stream = stream;
            _logger = logger;
        }

        public void Add(TelegramSession session)
        {
            List<TelegramSession> list = new List<TelegramSession>(GetAll());
            list.Add(session);
            SaveAsync(list.ToArray());
        }

        private async void SaveAsync(TelegramSession[] sessions)
        {
            _stream.Seek(0, SeekOrigin.Begin);
            try
            {
                JsonSerializer.Serialize(_stream, sessions);
                await _stream.FlushAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public TelegramSession[] GetAll()
        {
            TelegramSession[]? sessions = null;

            _stream.Seek(0, SeekOrigin.Begin);
            try
            {
                if (_stream.Length > 0)
                {
                    sessions = JsonSerializer.Deserialize<TelegramSession[]>(_stream);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return sessions ?? new TelegramSession[0];
        }

        public bool TryGetChatId(string userName, out long? chatId)
        {
            chatId = default;

            var items = GetAll();

            var found = items.FirstOrDefault(f => f.UserName.Equals(userName.Trim(), StringComparison.InvariantCultureIgnoreCase));

            if (found != null)
            {
                chatId = found.ChatId;
                return true;
            }

            return false;
        }
    }
}
