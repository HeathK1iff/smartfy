using System.Configuration;

namespace Smartfy.TelegramBot.Configuration.Impl
{
    internal class TelegramConfigurationSection : ConfigurationSection
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

        [ConfigurationProperty("sessions", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SessionCollection))]
        public SessionCollection Sessions
        {
            get
            {
                return (SessionCollection)this["sessions"];
            }
            set
            {
                this["sessions"] = value;
            }
        }
    }
}