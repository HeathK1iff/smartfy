using Smartfy.Core.Utils;
using Smartfy.Device.Configuration.Impl;

namespace Smartfy.Device.Configuration.Utils
{
    internal class DeviceConfigurationAdapter : IDeviceConfiguration
    {
        private readonly IDeviceConfiguration _section;

        public DeviceConfigurationAdapter(System.Configuration.Configuration configuration)
        {
            string basefolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration");

            if (!Directory.Exists(basefolder))
            {
                Directory.CreateDirectory(basefolder);
            }

            _section = configuration.GetOrCreateSection<DeviceConfigurationSection>("device", init =>
            {
                init.DevicesPath = System.IO.Path.Combine(basefolder, "devices.json");
            });
        }

        public string DevicesPath 
        {
            get => _section.DevicesPath; 
            set => _section.DevicesPath = value; 
        }
    }
}
