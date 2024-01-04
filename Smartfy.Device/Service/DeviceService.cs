using Smartfy.Device.Utils;
using Smartfy.Device.Entity;
using Microsoft.Extensions.Logging;

namespace Smartfy.Device.Service
{
    internal class DeviceService : IDeviceService
    {
        private readonly ILogger _logger;
        private readonly IDeviceFactory _factory;
        private readonly IDeviceRepository _repository;
        private List<BaseDevice> _devices = new();
        public DeviceService(ILoggerFactory loggerFactory, IDeviceFactory factory, IDeviceRepository repository)
        {
            _factory = factory;
            _repository = repository;
            _logger = loggerFactory.CreateLogger<DeviceService>();
            Load(_repository);
        }

        private void Load(IDeviceRepository repository)
        {
            foreach (var item in _repository.GetAll())
            {
                if (!Guid.TryParse(item.Id, out var deviceId))
                {
                    deviceId = Guid.NewGuid();
                    _logger.LogInformation($"Id is not defined. Generated a new Id ({deviceId}) for {item.Vendor}:{item.Model}");
                }

                var device = _factory.CreateDevice(deviceId, item.Vendor, item.Model, item.Location, item.ConnectionString);
                if (device != null)
                {
                    _devices.Add(device);
                }
            }
        }
   
        public BaseDevice[] GetAll()
        {
            return _devices.ToArray();
        }

        public void Remove(BaseDevice device)
        {
            _devices.Remove(device);
        }

        public BaseDevice Create(string vendor, string model, string location, string connectionString)
        {
            Guid newGuid = Guid.NewGuid();
            var newDefenition = new DeviceDef()
            {
                Id = newGuid.ToString(),
                Vendor = vendor,
                Model = model,
                ConnectionString = connectionString
            };

            var newDevice = _factory.CreateDevice(newGuid, vendor, model, location, connectionString);
            _repository.Add(newDefenition);

            return newDevice;
        }
    }
}
