namespace Smartfy.Device.Entity
{
    public interface IValue<T>
    {
        DateTime Stamp { get; }
        
        T GetValue();

        void SetValue(T value);
    }
}
