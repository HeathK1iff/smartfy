using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Weather.Configuration.Utils;
using Smartfy.Weather.Services;

namespace Smartfy.TelegramBot
{
    public static class Library
    {
        public static void Init(System.Configuration.Configuration configuration, 
            ILoggerFactory loggerFactory, 
            IServiceCollection services)
        {
            services.AddService<IWeatherService>(new WeatherService(new WeatherConfigurationAdapter(configuration), 
                loggerFactory.CreateLogger<WeatherService>(), services));
        }
    }
}
