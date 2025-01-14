namespace Smartfy.Device.Entity
{
    public class TrackedValue<T> : ObservableValue<T>
    {
        private readonly int _capacity;
        private Queue<IValue<T>> _values = new();

        public TrackedValue(int capacity)
        {
            _capacity = capacity;
        }

        public override T GetValue()
        {
            IValue<T> lastValue = default(IValue<T>);
            try
            {
                lastValue = _values.Peek();
            } catch (InvalidOperationException e)
            { 
            }

            if (lastValue != null)
            {
                return lastValue.GetValue();
            }
            return default(T);
        }

        public override void SetValue(T value)
        {
            while (_values.TryDequeue(out var item))
            {
                if (_capacity > _values.Count)
                    break;
            }

            _values.Enqueue(createValue(value));

            base.SetValue(value);
        }

        private Value<T> createValue(T value)
        {
            var val = new Value<T>();
            val.SetValue(value);
            return val;
        }
    }

}
