using Smartfy.Core.Services;
using Smartfy.Weather.Entity;

namespace Smartfy.Weather.Services
{
    public interface IWeatherService: IService
    {
        DateTime Sunrise { get; }

        DateTime Sunset { get; }

        WeatherForecast[] Forecast { get; }

        CurrentWeather Current { get; }

        void Refresh();
    }
}
