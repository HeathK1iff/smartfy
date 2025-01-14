namespace Smartfy.Device.Entity
{
    public interface ITemperature
    {
        public TrackedValue<float> Temperature { get; }
    }
}
