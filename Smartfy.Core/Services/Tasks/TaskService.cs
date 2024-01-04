using Microsoft.Extensions.Logging;

namespace Smartfy.Core.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private ILoggerFactory _loggerFactory;
        private TaskDispatcher _dispatcher;

        public TaskService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;  
            _dispatcher = new TaskDispatcher(_loggerFactory.CreateLogger<TaskDispatcher>());
        }

        public void Add(ITask task)
        {
            _dispatcher.Add(task);
        }

        public void Start()
        {
            _dispatcher.Start();
        }
    }
}
