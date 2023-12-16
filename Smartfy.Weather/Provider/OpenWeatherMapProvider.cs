using OpenWeatherMap.Standard.Enums;
using OpenWeatherMap.Standard;
using Smartfy.Weather.Entity;
using Smartfy.Weather.Exceptions;
using Microsoft.Extensions.Logging;

namespace Smartfy.Weather.Provider
{
    public class OpenWeatherMapProvider : IWeatherProvider
    {
        private string _token;
        private int _cityId;
        private int _timeZone;
        private ILogger _logger;

        public OpenWeatherMapProvider(string token, int cityId,
            int timeZone, ILogger logger)
        {
            _token = token;
            _cityId = cityId;
            _timeZone = timeZone;
            _logger = logger;
        }

        public void Refresh()
        {
            Current currentWeather = new Current(_token, WeatherUnits.Metric)
            {
                Languages = Languages.Russian,
                FetchIcons = false,
                ForecastTimestamps = 5
            };

            var weatherData = currentWeather.GetWeatherDataByCityId(_cityId).Result;

            
            Current = new WeatherInfo()
            {
                Pressure = weatherData.WeatherDayInfo.Pressure,
                Humidity = weatherData.WeatherDayInfo.Humidity,
                Temperature = weatherData.WeatherDayInfo.Temperature,
                Clouds = weatherData.Clouds.All,
                IsRain = weatherData.Weathers.Length > 0,
                Wind = weatherData.Wind.Speed
            };

            Sunrise = weatherData.DayInfo.Sunrise.AddHours(_timeZone);
            Sunset = weatherData.DayInfo.Sunset.AddHours(_timeZone);

            _logger.LogDebug($"Sunrise={Sunrise.ToLongDateString()};Sunset={Sunset.ToLongDateString()};");
            _logger.LogDebug($"Temp={Current.Temperature};Hum={Current.Humidity};"+
                $"Pres={Current.Pressure};Clouds={Current.Clouds};IsRaid={Current.IsRain};Wind={Current.Wind}");

            var forecastWeatherData = currentWeather.GetForecastDataByCityId(_cityId);

            var forecasts = forecastWeatherData.
                WeatherData.
                Where(f => f.AcquisitionDateTime > DateTime.Now).
                OrderBy(f => f.AcquisitionDateTime);

            Forecast = new WeatherForecast[forecasts.Count()];
            int i = 0;
            foreach (var forecast in forecasts)
            {
                Forecast[i] = new WeatherForecast()
                {
                    Date = forecast.AcquisitionDateTime,
                    Temperature = forecast.WeatherDayInfo.Temperature,
                    Humidity = forecast.WeatherDayInfo.Humidity,
                    IsRain = (forecast.Rain?.LastThreeHours ?? 0) > 0,
                    Clouds = forecast.Clouds.All,
                    Pressure = forecast.WeatherDayInfo.Pressure,
                    Wind = forecast.Wind.Speed
                };
                _logger.LogDebug($"Date={Forecast[i].Date.ToLongDateString()};Temp={Forecast[i].Temperature};"+
                                 $"Hum={Forecast[i].Humidity};Rain={Forecast[i].IsRain};"+
                                 $"Clouds={Forecast[i].Clouds};Pressure={Forecast[i].Pressure};Wind={Forecast[i].Wind}");

                i++;
            }
        }

        public DateTime Sunrise { get; private set; }
        public DateTime Sunset { get; private set; }
        public WeatherInfo Current { get; private set; }
        public WeatherForecast[] Forecast { get; private set; } = Array.Empty<WeatherForecast>();
    }
}