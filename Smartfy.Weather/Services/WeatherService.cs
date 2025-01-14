using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Weather.Configuration;
using Smartfy.Weather.Entity;
using Smartfy.Weather.Exceptions;
using Smartfy.Weather.Provider;

namespace Smartfy.Weather.Services
{
    public class WeatherService : IWeatherService
    {
        private IWeatherConfiguration _configuration;
        private ILogger _logger;
        private IServiceCollection _services;
        private IWeatherProvider _weatherProvider;

        public WeatherService(IWeatherConfiguration configuration, ILogger logger, IServiceCollection services)
        {
            _logger = logger;
            _services = services;
            _configuration = configuration;

            if ((string.IsNullOrWhiteSpace(_configuration.Token)) || (_configuration.CityId <= 0))
            {
                throw new InvalidConfigurationException();
            }

            _weatherProvider = new OpenWeatherMapProvider(_configuration.Token, _configuration.CityId, _configuration.TimeZone, _logger);
        }

        public DateTime Sunrise
        {
            get => _weatherProvider.Sunrise;
        }

        public DateTime Sunset
        {
            get => _weatherProvider.Sunset;
        }

        public WeatherForecast[] Forecast
        {
            get => _weatherProvider.Forecast;
        }

        public CurrentWeather Current
        {
            get
            {
                return new CurrentWeather()
                {
                    Clouds = new WeatherValue<int>()
                    {
                        Value = _weatherProvider.Current.Clouds
                    },
                    IsRain = new WeatherValue<bool>()
                    {
                        Value = _weatherProvider.Current.IsRain
                    },
                    Pressure = new WeatherValue<float>()
                    {
                        Value = _weatherProvider.Current.Pressure
                    },
                    Wind = new WeatherValue<float>()
                    {
                        Value = _weatherProvider.Current.Wind
                    },
                    Temperature = GetActualTemperature(),
                    Humidity = GetActualHumidity()
                };
            }
        }

        public void Refresh()
        {
            if (_weatherProvider != null)
            {
                _weatherProvider.Refresh();
            }
        }

        private WeatherValue<float> GetActualTemperature()
        {
            return new WeatherValue<float>()
            {
                Value = _weatherProvider.Current.Temperature
            };
        }

        private WeatherValue<int> GetActualHumidity()
        {
            return new WeatherValue<int>()
            {
                Value = _weatherProvider.Current.Humidity
            };
        }

        protected virtual DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
