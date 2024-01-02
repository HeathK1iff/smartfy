using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Device.Configuration.Utils;
using Smartfy.Device.Service;
using Smartfy.Device.Utils;
using Smartfy.Runner;

namespace Smartfy.Device
{
    public static class Library
    {
        public static void Init(System.Configuration.Configuration configuration,
            ILoggerFactory loggerFactory,
            IServiceCollection services)
        {
            var config = new DeviceConfigurationAdapter(configuration);
            var messageService = services.GetService<IMessageService>();
            IDeviceFactory deviceFactory = new DeviceFactory(loggerFactory, messageService);
            
            var library = new ExternalLibraryLoader(loggerFactory, "Device", "Init");
            library.LoadAndInitAll(config.Path, loggerFactory, deviceFactory);

            var repository = new DeviceRepository(new FileStream(Path.Combine(config.Path, "device.json"), FileMode.OpenOrCreate, FileAccess.ReadWrite));
            services.AddService<IDeviceService>(new DeviceService(loggerFactory, deviceFactory, repository));
        }
    }
}