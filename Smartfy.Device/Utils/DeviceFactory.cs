using Microsoft.Extensions.Logging;
using Smartfy.Core.Services.Messages;
using Smartfy.Device.Entity;
using Smartfy.Device.Exception;

namespace Smartfy.Device.Utils
{
    public class DeviceFactory : IDeviceFactory
    {
        private Dictionary<Tuple<string, string>, Type> _register = new();
        private readonly IMessageService _broker;
        private readonly ILogger _logger;

        public DeviceFactory(ILoggerFactory loggerFactory, IMessageService broker)
        {
            _logger = loggerFactory.CreateLogger<DeviceFactory>(); ;
            _broker = broker;
        }

        public void Register(string vendor, string model, Type type)
        {
            if (!type.IsSubclassOf(typeof(BaseDevice)))
            {
                throw new InvalidTypeException();
            }

            var key = CreateKey(vendor, model);

            if (_register.ContainsKey(key))
            {
                throw new AlreadyTypeExistException();
            }

            _register.Add(key, type);

            _logger.LogInformation($"Registered device class for {vendor}:{model}");
        }

        public BaseDevice? CreateDevice(Guid id, string vendor, string model, string location, string connectionString)
        {
            var key = CreateKey(vendor, model);

            if (!_register.TryGetValue(key, out var type))
            {
                return null;
            }

            _logger.LogInformation($"Created device for {vendor}:{model}:{id.ToString()}");
            return Activator.CreateInstance(type, _broker, id, vendor, model, location, connectionString) as BaseDevice ?? BaseDevice.CreateEmptyDevice();
        }

        private Tuple<string, string> CreateKey(string vendor, string model)
        {
            return Tuple.Create(vendor.ToUpper(), model.ToUpper());
        } 
    }
}