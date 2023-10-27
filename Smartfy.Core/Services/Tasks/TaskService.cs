using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Smartfy.Core.Services.Tasks
{
    public class TaskService : ITaskService, IEnumerable<ITask>
    {
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private Dictionary<Guid, Task> _tasks = new();
        private IServiceCollection _services;
        public TaskService(ILoggerFactory loggerFactory, IServiceCollection services)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<TaskService>();   
            _services = services;
        }

        public Guid Add(ITask task)
        {
            var taskItem = new Task(task, _services, _loggerFactory);
            _tasks.Add(taskItem.Id, taskItem);
            _logger.LogInformation($"Added a new task {taskItem.Id} to scheduler");
            taskItem.Prepare();
            return taskItem.Id;
        }

        public void Execute(Guid id)
        {
            if (_tasks.TryGetValue(id, out Task? task))
            {
                task?.Execute();
            }
        }

        public IEnumerator<ITask> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tasks.ToArray().GetEnumerator();
        }
    }
}
