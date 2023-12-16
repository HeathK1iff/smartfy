using Microsoft.Extensions.Logging;
using Smartfy.Core.Entities;
using Smartfy.Core.Messages.Strategies.Utils;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Core.Services.Tasks;
using Smartfy.Weather.Services;
using System.Text;

namespace Smartfy.Runner.Tasks
{
    internal class WeatherTask : ITask
    {
        private static string WeatherRecepientGroup = "weather"; 
        private ILogger _logger;
        private IServiceCollection _services;

        public WeatherTask(IServiceCollection services, ILogger logger)
        {
            _services = services;
            _logger = logger;
        }
    
        public bool Exeсute()
        {
            var weatherService = _services.GetService<IWeatherService>();
            var messageService = _services.GetService<IMessageService>();
            
            weatherService.Refresh();

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine(GetTitleByCurrentTime(weatherService.Sunrise));
            messageBuilder.AppendLine($"На улице сейчас {GetCloudStatusByPercent(weatherService.Current.Clouds.Value)}:");
            messageBuilder.AppendLine($"🌡 Температура: {weatherService.Current.Temperature.Value} °C");
            messageBuilder.AppendLine($"🎚Влажность: {weatherService.Current.Humidity.Value} %");
            messageBuilder.AppendLine($"⚗️Давление: {weatherService.Current.Pressure.Value} гПа");
            if (weatherService.Current.Wind.Value > 0)
            {
                messageBuilder.AppendLine($"💨Ветер: {weatherService.Current.Wind.Value} м/c");
            }
            _services.GetService<IMessageService>().Routes.AddRouteIfNotExist(new Route()
            {
                Group = WeatherRecepientGroup,
                Recepient = ""
            });

            messageService.Publish(new GroupOutputMessage()
            {
                Data = messageBuilder.ToString(),
                RecepientGroups = new string[] { WeatherRecepientGroup }
            });

            return true;
        }

        protected virtual DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        private string GetTitleByCurrentTime(DateTime sunrise)
        {
            var hour = GetCurrentTime().Hour;

            _logger.LogDebug($"Sunrise time = {sunrise.ToShortTimeString()}");

            if ((hour >= sunrise.Hour) && (hour < 10))
            {
                return "Доброе утро";
            }
            else
            if ((hour >= 10) && (hour < 16))
            {
                return "Добрый день";
            }
            else
            if ((hour >= 16) && (hour < 23))
            {
                return "Добрый вечер";
            }

            return "Доброй ночи";
        }

        private string GetCloudStatusByPercent(int percent)
        {
            if ((percent >= 10) && (percent < 35))
            {
                return "малооблачно 🌤";
            }
            else
            if ((percent >= 35) && (percent < 70))
            {
                return "переменная облачность ⛅️";
            }
            else
            if ((percent >= 80) && (percent < 90))
            {
                return "облачно c прояснениями ⛅️";
            }
            if ((percent >= 90) && (percent <= 100))
            {
                return "облачно ☁️";
            }

            return "ясно ☀️";
        }
    }
}
