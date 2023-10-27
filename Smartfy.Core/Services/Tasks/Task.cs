using Microsoft.Extensions.Logging;
using Smartfy.Core.Exceptions;

namespace Smartfy.Core.Services.Tasks
{ 
    public class Task
    {
        private ILogger _logger;
        private ILoggerFactory _loggerFactory;
        private TaskException? _exception;
        private IServiceCollection _services;
        private Guid _id;
        private ITask _task;
        private long _executionCounter;
        private DateTime _lastSuccessExecuted;

        public Task(ITask task, IServiceCollection services, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<Task>();
            _id = Guid.NewGuid();
            _task = task;
            _services = services;
            _logger.LogInformation($"Created task ({Id})");
        }

        public TaskException? GetLastException()
        {
            return _exception;
        }

        public Guid Id => _id;

        public Tasks.TaskStatus Status { get; private set; } = Tasks.TaskStatus.Created;
        
        public void Prepare()
        {
            try
            {
                _task.Prepare(Execute, _loggerFactory);
                Status = Tasks.TaskStatus.Prepared;
                _logger.LogInformation($"Task {Id} is prepared");  
            }
            catch (System.Exception e)
            {
                ThrowException(e);
            }
        }

        public long ExecutionCounter => _executionCounter;

        public DateTime LastSuccessExecuted => _lastSuccessExecuted;

        public void Execute()
        {
            try
            {
                Status = TaskStatus.Executing;
                try
                { 
                    bool success = true; 
                    _task.Exeсute(_services, ref success);
                    _executionCounter++;
                    if (success)
                    {
                        _lastSuccessExecuted = DateTime.Now;
                    }
                } 
                finally
                {
                    Status = Tasks.TaskStatus.Executed;
                    _logger.LogInformation($"Task {Id} is executed");
                }
            }
            catch (System.Exception e)
            {
                ThrowException(e);
            }
        }

        private void ThrowException(System.Exception e)
        {
            Status = Tasks.TaskStatus.Error;
            _exception = new TaskException(e.Message, e);
            _logger.LogError(_exception, _exception.Message);
        }
    }
}
