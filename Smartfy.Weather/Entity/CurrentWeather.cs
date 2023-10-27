namespace Smartfy.Weather.Entity
{
    public struct CurrentWeather
    {
        public WeatherValue<int> Clouds { get; init; }
        public WeatherValue<bool> IsRain { get; init; }
        public WeatherValue<float> Temperature { get; init; }
        public WeatherValue<int> Humidity { get; init; }
        public WeatherValue<float> Pressure { get; init; }
        public WeatherValue<float> Wind { get; init; }
    }
}
