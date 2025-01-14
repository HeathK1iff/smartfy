using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Smartfy.Core.Services.Tasks
{
    internal class TaskDispatcher
    {
        public const int DefaultInterval = 60000;
        private ILogger _logger;
        private CancellationTokenSource _cts = new();
        private ConcurrentQueue<ITask> _tasks = new();
        private int _interval;

        public TaskDispatcher(ILogger logger, int interval = TaskDispatcher.DefaultInterval)
        {
            _logger = logger;
            _interval = interval;
        }

        public void Add(ITask task)
        {
            _tasks.Enqueue(task);
        }

        public void Start()
        {
            var thread = new Thread((object? obj) =>
            {
                if (obj is CancellationToken token)
                {
                    _logger.LogInformation($"TaskDispatcher is started");

                    using (var resetEvent = new PeriodicalManualResetEvent(false, _interval))
                    {
                        while (!token.IsCancellationRequested)
                        {
                            var delayed = new Queue<ITask>();
                            while (_tasks.TryDequeue(out var task))
                            {
                                try
                                {
                                    bool successExecuted = task.Exeсute();
                                    if (!successExecuted)
                                        delayed.Enqueue(task);

                                    if (successExecuted)
                                        _logger.LogInformation($"Task is executed");
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"Task execution error: {ex.Message}", ex);
                                }
                            }

                            while (delayed.TryDequeue(out var task))
                            {
                                _tasks.Enqueue(task);
                            }

                            resetEvent.WaitOne();
                        }
                    }
                }
            });

            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Lowest;
            thread.Start(_cts.Token);
        }

        public void Stop()
        {
            _cts.Cancel();
        }
    }
}
