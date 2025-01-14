using Smartfy.Core.Utils;
using Smartfy.Mqtt.Configuration.Impl;

namespace Smartfy.Mqtt.Configuration.Utils
{
    internal class MqttConfigurationAdapter : IMqttConfiguration
    {
        private MqttConfigurationSection _section;
        public MqttConfigurationAdapter(System.Configuration.Configuration configuration)
        {
            _section = configuration.GetOrCreateSection<MqttConfigurationSection>("mqtt", item =>
            {
                item.SubsribeTopic = "#";
                item.ClientId = Guid.NewGuid().ToString();
            });
        }

        public string ClientId
        {
            get
            {
                return _section.ClientId;
            }
            set
            {
                _section.ClientId = value;
            }
        }
        public string Host
        {
            get
            {
                return _section.Host;
            }
            set
            {
                _section.Host = value;
            }
        }
        public string SubsribeTopic
        {
            get
            {
                return _section.SubsribeTopic;
            }
            set
            {
                _section.SubsribeTopic = value;
            }
        }
    }
}
