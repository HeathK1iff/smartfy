using Microsoft.Extensions.Logging;
using Smartfy.Device.Utils;
using Smartfy.Device.Xiaomi.Devices;

namespace Smartfy.Device.Xiaomi
{
    public static class Device
    {
        private static ILoggerFactory _loggerFactory;
        public static void Init(ILoggerFactory loggerFactory,
           IDeviceRegister register)
        {
            _loggerFactory = loggerFactory;
            register.Register("Xiaomi", "WSDCGQ11LM", typeof(WSDCGQ11LM));
            register.Register("Xiaomi", "MCCGQ11LM", typeof(MCCGQ11LM));
            register.Register("Xiaomi", "WSDCGQ01LM", typeof(WSDCGQ01LM));
        }

        public static ILoggerFactory LoggerFactory => _loggerFactory;
    }
}