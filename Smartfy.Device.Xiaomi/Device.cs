using Microsoft.Extensions.Logging;
using Smartfy.Device.Utils;

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
            
        }

        public static ILoggerFactory LoggerFactory => _loggerFactory;
    }
}