using System.Configuration;
using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Core.Services.Tasks;
using Smartfy.Runner.Tasks;

namespace Smartfy.Runner
{
    public class Program
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceCollection _services;
        private readonly Configuration _configuration;
        public static void Main(string[] args)
        {
            var program = new Program();
            program.RunService();
        }

        public Program()
        {
            _services = new Services();
            _configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                //builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddFile("smartfy.log", LogLevel.Debug);
            });

            _services.AddService<IMessageService>(new MessageService(_configuration));
            _services.AddService<ITaskService>(new TaskService(_loggerFactory));
        }

        public void RunService()
        {
            var libraryLoader = new LibraryLoader(AppDomain.CurrentDomain.BaseDirectory,
                _configuration, _loggerFactory, _services);
            libraryLoader.LoadAll();

            var weatherTask = new WeatherTask(_services, _loggerFactory.CreateLogger<WeatherTask>());
            _services.GetService<ITaskService>().Add(new PeriodicalTask(7, 0, weatherTask));
            _services.GetService<ITaskService>().Add(new PeriodicalTask(13, 0, weatherTask));
            _services.GetService<ITaskService>().Add(new PeriodicalTask(18, 0, weatherTask));

            var dailyMarkTask = new DailyMarksTask(_services, _loggerFactory.CreateLogger<DailyMarksTask>());
            _services.GetService<ITaskService>().Add(new PeriodicalTask(18, 0, dailyMarkTask));

            var publicCalendarTask = new PublicCalendarTask(_services, _loggerFactory.CreateLogger<PublicCalendarTask>());
            _services.GetService<ITaskService>().Add(new PeriodicalTask(8, 0, publicCalendarTask));
        }
    }
}