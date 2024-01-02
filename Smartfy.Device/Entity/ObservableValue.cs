namespace Smartfy.Device.Entity
{
    public abstract class ObservableValue<T> : IValue<T>, IObservable<IValue<T>>
    {
        private HashSet<IObserver<IValue<T>>> _observers = new();
        private DateTime _stamp;
        public DateTime Stamp => _stamp;

        public IDisposable Subscribe(IObserver<IValue<T>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }

        public abstract T GetValue();

        public virtual void SetValue(T value)
        {
            _stamp = DateTime.Now;
            NotifyAll();
        }

        private void NotifyAll()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(this);
            }
        }


        private class Unsubscriber : IDisposable
        {
            private HashSet<IObserver<IValue<T>>> _observers;
            private IObserver<IValue<T>> _observer;
            public Unsubscriber(HashSet<IObserver<IValue<T>>> observers, IObserver<IValue<T>> observer)
            {
                _observer = observer;
                _observers = observers;
            }

            public void Dispose()
            {
                _observers.Remove(_observer);
                _observer = null;
            }
        }
    }
}
