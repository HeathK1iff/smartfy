using System.Configuration;

namespace Smartfy.Device.Configuration.Impl
{
    public class DeviceConfigurationSection : ConfigurationSection, IDeviceConfiguration
    {
        [ConfigurationProperty("path")]
        public string Path 
        {
            get
            {
                return this["path"] as string ?? string.Empty;
            }
            
            set
            {
                this["path"] = value;
            } 
        }
    }
}
