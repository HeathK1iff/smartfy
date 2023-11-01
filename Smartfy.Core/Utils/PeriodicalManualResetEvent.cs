namespace Smartfy.Core.Services.Tasks
{
    internal class PeriodicalManualResetEvent : EventWaitHandle
    {
        private bool _disposedValue;
        private int _interval;
        private Timer? _timer;

        public PeriodicalManualResetEvent(bool initialState, int interval) : base(initialState, EventResetMode.ManualReset)
        {
            _interval = interval;
        }

        public override bool WaitOne()
        {
            if (_timer == null)
            {
                _timer = new Timer(ResetStateHandler, this, _interval, _interval);
            }

            base.Reset();
            return base.WaitOne();
        }

        protected override void Dispose(bool explicitDisposing)
        {
            if (!_disposedValue)
            {
                if (explicitDisposing)
                {
                    _timer?.Dispose();
                    _timer = null;
                }

                _disposedValue = true;
            }

            base.Dispose(explicitDisposing);
        }

        private void ResetStateHandler(object? state)
        {
            if (state is EventWaitHandle handler)
            {
                handler.Set();
            }
        }
    }
}
