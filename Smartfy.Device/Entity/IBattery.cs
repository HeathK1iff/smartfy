namespace Smartfy.Device.Entity
{
    public interface IBattery
    {
        public TrackedValue<int> Battery { get; }
    }
}
