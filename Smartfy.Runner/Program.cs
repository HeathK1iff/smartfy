using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Core.Services.Tasks;
using Smartfy.Mqtt.Services;
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
            _configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddFile(Path.Combine(logFolder, "smartfy.log"), LogLevel.Information);
            });

            _services = new Services(_loggerFactory.CreateLogger<Services>());

            _services.AddService<IMessageService>(new MessageService(_configuration));
            _services.AddService<ITaskService>(new TaskService(_loggerFactory));
        }

        public void RunService()
        {
            var libraryLoader = new ExternalLibraryLoader(_loggerFactory, "Library", "Init");

            libraryLoader.LoadAndInitAll(AppDomain.CurrentDomain.BaseDirectory,
                _configuration, _loggerFactory, _services);

            var weatherTask = new WeatherTask(_services, _loggerFactory.CreateLogger<WeatherTask>());
            _services.GetService<ITaskService>().Add(new PeriodicalTask(7, 0, weatherTask, _loggerFactory.CreateLogger<PeriodicalTask>()));
            _services.GetService<ITaskService>().Add(new PeriodicalTask(13, 0, weatherTask, _loggerFactory.CreateLogger<PeriodicalTask>()));
            _services.GetService<ITaskService>().Add(new PeriodicalTask(18, 0, weatherTask, _loggerFactory.CreateLogger<PeriodicalTask>()));


            var task = new HomeClimatTask(_services, _loggerFactory.CreateLogger<HomeClimatTask>());
            _services.GetService<ITaskService>().Add(new PeriodicalTask(0, 0, task, _loggerFactory.CreateLogger<PeriodicalTask>()));
            _services.GetService<ITaskService>().Add(new PeriodicalTask(4, 0, task, _loggerFactory.CreateLogger<PeriodicalTask>()));
            _services.GetService<ITaskService>().Add(new PeriodicalTask(8, 0, task, _loggerFactory.CreateLogger<PeriodicalTask>()));
            _services.GetService<ITaskService>().Add(new PeriodicalTask(12, 0, task, _loggerFactory.CreateLogger<PeriodicalTask>()));
            _services.GetService<ITaskService>().Add(new PeriodicalTask(16, 0, task, _loggerFactory.CreateLogger<PeriodicalTask>()));
            _services.GetService<ITaskService>().Add(new PeriodicalTask(20, 0, task, _loggerFactory.CreateLogger<PeriodicalTask>()));

            _services.GetService<ITaskService>().Start();
            _services.GetService<IMqttService>().Start();
        }
    }
}