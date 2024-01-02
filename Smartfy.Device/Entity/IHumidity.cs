namespace Smartfy.Device.Entity
{
    public interface IHumidity
    {
        public TrackedValue<float> Humidity { get; }
    }
}
