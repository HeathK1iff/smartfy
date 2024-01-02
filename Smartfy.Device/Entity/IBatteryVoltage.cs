namespace Smartfy.Device.Entity
{
    public interface IBatteryVoltage
    {
        public TrackedValue<int> BatteryVoltage { get; }
    }
}
