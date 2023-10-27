namespace Smartfy.Weather.Entity
{
    public struct WeatherInfo
    {
        public int Clouds { get; init; }
        public bool IsRain { get; init; }
        public float Temperature { get; init; }
        public int Humidity { get; init; }
        public float Pressure { get; init; }
        public float Wind { get; init; }
    }
}
