using Smartfy.Core.Utils;
using Smartfy.Device.Configuration.Impl;

namespace Smartfy.Device.Configuration.Utils
{
    internal class DeviceConfigurationAdapter : IDeviceConfiguration
    {
        private readonly IDeviceConfiguration _section;

        public DeviceConfigurationAdapter(System.Configuration.Configuration configuration)
        {

            string basefolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "device");

            if (!Directory.Exists(basefolder))
            {
                Directory.CreateDirectory(basefolder);
            }

            _section = configuration.GetOrCreateSection<DeviceConfigurationSection>("device", init =>
            {
                init.Path = basefolder;
            });
        }

        public string Path 
        {
            get => _section.Path; 
            set => _section.Path = value; 
        }
    }
}
