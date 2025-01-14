using System.Configuration;

namespace Smartfy.Mqtt.Configuration.Impl
{
    internal class MqttConfigurationSection : ConfigurationSection, IMqttConfiguration
    {
        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get
            {
                return (string)this["host"] ?? string.Empty;
            }
            set
            {
                this["host"] = value;
            }
        }

        [ConfigurationProperty("clientId", IsRequired = true)]
        public string ClientId
        {
            get
            {
                return (string)this["clientId"] ?? string.Empty;
            }
            set
            {
                this["clientId"] = value;
            }
        }

        [ConfigurationProperty("subsribeTopic", IsRequired = true)]
        public string SubsribeTopic
        {
            get
            {
                return (string)this["subsribeTopic"] ?? string.Empty;
            }
            set
            {
                this["subsribeTopic"] = value;
            }
        }
    }
}
