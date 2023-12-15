using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Core.Utils;
using Smartfy.Weather.Configuration.Impl;
using Smartfy.Weather.Configuration.Utils;
using Smartfy.Weather.Services;
using System.Configuration;

namespace Smartfy.TelegramBot
{
    public sealed class LibraryLoader
    {
        public static void CreateService(System.Configuration.Configuration configuration, 
            ILoggerFactory loggerFactory, 
            IServiceCollection services)
        {
            services.AddService<IWeatherService>(new WeatherService(new WeatherConfigurationAdapter(configuration), 
                loggerFactory.CreateLogger<WeatherService>(), services));
        }
    }
}
