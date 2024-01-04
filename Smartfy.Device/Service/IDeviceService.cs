using Smartfy.Core.Services;
using Smartfy.Device.Entity;

namespace Smartfy.Device.Service
{
    public interface IDeviceService : IService
    {
        BaseDevice Create(string vendor, string model, string location, string connectionString);
        void Remove(BaseDevice device);
        BaseDevice[] GetAll();
    }
}
