namespace Smartfy.Device.Utils
{
    public interface IDeviceRegister
    {
        void Register(string vendor, string model, Type type);
    }
}