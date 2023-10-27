using System.Configuration;

namespace Smartfy.Weather.Configuration
{
    public interface IWeatherConfiguration
    {
        public string Token { get; }
        public int CityId { get; }
        public int TimeZone { get; }
    }
}
