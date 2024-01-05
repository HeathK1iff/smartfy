using System.Configuration;

namespace Smartfy.TelegramBot.Configuration.Impl
{
    internal class TelegramConfigurationSection : ConfigurationSection, ITelegramConfiguration
    {

        [ConfigurationProperty("token", IsRequired = true)]
        public string Token
        {
            get
            {
                return this["token"] as string ?? string.Empty;
            }
            set
            {
                this["token"] = value;
            }
        }

        [ConfigurationProperty("sessions", IsRequired = true)]
        public string SessionsPath
        {
            get
            {
                return this["sessions"] as string ?? string.Empty;
            }
            set
            {
                this["sessions"] = value;
            }
        }
    }
}