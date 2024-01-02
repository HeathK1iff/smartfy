using Smartfy.Device.Entity;

namespace Smartfy.Device.Utils
{
    public interface IDeviceRepository
    {
        void Add(DeviceDef deviceDef);
        void Remove(DeviceDef deviceDef);
        DeviceDef[] GetAll();
    }
}
