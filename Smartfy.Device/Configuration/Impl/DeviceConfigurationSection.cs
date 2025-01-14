using System.Configuration;

namespace Smartfy.Device.Configuration.Impl
{
    public class DeviceConfigurationSection : ConfigurationSection, IDeviceConfiguration
    {
        [ConfigurationProperty("devices")]
        public string DevicesPath 
        {
            get
            {
                return this["devices"] as string ?? string.Empty;
            }
            
            set
            {
                this["devices"] = value;
            } 
        }
    }
}
