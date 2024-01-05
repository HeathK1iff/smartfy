using Smartfy.Core.Utils;
using Smartfy.TelegramBot.Configuration.Impl;

namespace Smartfy.TelegramBot.Configuration.Adapter
{
    internal class TelegramConfigurationAdapter : ITelegramConfiguration
    {
        private System.Configuration.Configuration _configuration;
        private TelegramConfigurationSection _section;
        private ISessions? _sessions;

        public TelegramConfigurationAdapter(System.Configuration.Configuration configuration)
        {
            _configuration = configuration;

            string basefolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration");

            if (!Directory.Exists(basefolder))
            {
                Directory.CreateDirectory(basefolder);
            }

            _section = _configuration.GetOrCreateSection<TelegramConfigurationSection>("telegram", item =>
            {
                item.Token = string.Empty;
                item.SessionsPath = Path.Combine(basefolder, "telegram_sessions.json");
            });
        }

        public string Token => _section.Token;

        public string SessionsPath => _section.SessionsPath;

        public void Save()
        {
            _configuration.Save();
        }
    }
}