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
            _section = _configuration.GetOrCreateSection<TelegramConfigurationSection>("telegram", item =>
            {
                item.Token = string.Empty;
            });

        }

        public string Token => _section.Token;

        public ISessions Sessions
        {
            get
            {
                if (_sessions == null)
                {
                    _sessions = new SessionsAdapter(_section.Sessions);
                }

                return _sessions;
            }
        }

        public void Save()
        {
            _configuration.Save();
        }
    }
}