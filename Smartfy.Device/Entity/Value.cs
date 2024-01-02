namespace Smartfy.Device.Entity
{
    public class Value<T> : ObservableValue<T>
    {
        private T _value;
        public override T GetValue()
        {
            return _value;
        }

        public override void SetValue(T value)
        {
            _value = value;
            base.SetValue(value);
        }
    }

}
