using Smartfy.Weather.Entity;

namespace Smartfy.Weather.Provider
{
    public interface IWeatherProvider
    {
        DateTime Sunrise { get; }
        DateTime Sunset { get; }
        WeatherInfo Current { get; }
        WeatherForecast[] Forecast { get; }

        void Refresh();
    }
}
