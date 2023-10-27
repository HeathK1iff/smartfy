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
                builder.AddConsole();
                builder.AddFile("smartfy.log");
                builder.SetMinimumLevel(LogLevel.Debug);
            });

            _services.AddService<IMessageService>(new MessageService(_configuration));
            _services.AddService<ITaskService>(new TaskService(_loggerFactory, _services));
        }

        public void RunService()
        {
            var libraryLoader = new LibraryLoader(AppDomain.CurrentDomain.BaseDirectory,
                _configuration, _loggerFactory, _services);
            libraryLoader.LoadAll();

            _services.GetService<ITaskService>().Add(new DailyTask(new WeatherTask(), 7, 0, true));
            _services.GetService<ITaskService>().Add(new DailyTask(new WeatherTask(), 13, 0, true));
            _services.GetService<ITaskService>().Add(new DailyTask(new WeatherTask(), 18, 0, true));

            _services.GetService<ITaskService>().Add(new DailyTask(new DailyMarksTask(), 15, 0, true));
            _services.GetService<ITaskService>().Add(new DailyTask(new DailyMarksTask(), 19, 0, true));
        }
    }
}