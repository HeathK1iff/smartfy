using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Tasks;

namespace Smartfy.Runner.Tasks
{
    public class DailyTask : ITask
    {
        private int _hour;
        private int _min;
        private bool _repeat;
        private ITask _task;
        
        public DailyTask(ITask task, int hour, int min, bool repeat) 
        {
            _hour = hour;
            _min = min;
            _repeat = repeat;
            _task = task;
        }

        public void Exeсute(IServiceCollection services, ref bool success)
        {
            _task.Exeсute(services, ref success);
        }

        public bool Prepare(Action? executeAction, ILoggerFactory loggerFactory)
        {
            ILogger logger = loggerFactory.CreateLogger<DailyTask>();

            DateTime dateNow = DateTime.Now;
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                _hour, _min, 0);

            TimeSpan ts;
            if (date > dateNow)
            {
                ts = date.Subtract(dateNow);
            }
            else
            {
                date = date.AddDays(1);
                ts = date.Subtract(dateNow);
            }

            _task.Prepare(null, loggerFactory);
            logger.LogDebug($"Task was scheduled for execute after ~{ Math.Round(ts.TotalMinutes)} min");
            
            System.Threading.Tasks.Task.Delay(ts).ContinueWith((task) =>
            {
                executeAction?.Invoke();
                
                if (_repeat)
                {
                    Prepare(executeAction, loggerFactory);
                }
            });

            return true;
        }
    }
}
