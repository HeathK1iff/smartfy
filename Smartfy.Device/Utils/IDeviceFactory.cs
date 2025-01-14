using Smartfy.Device.Entity;

namespace Smartfy.Device.Utils
{
    public interface IDeviceFactory: IDeviceRegister
    {
        BaseDevice? CreateDevice(Guid id, string vendor, string model, 
            string location, string connectionString);
    }
}