using Smartfy.Core.Utils;
using Smartfy.Weather.Configuration.Impl;

namespace Smartfy.Weather.Configuration.Utils
{
    internal class WeatherConfigurationAdapter: IWeatherConfiguration
    {
        private readonly WeatherConfigurationSection _section;
        public WeatherConfigurationAdapter(System.Configuration.Configuration configuration) 
        {
            _section = configuration.GetOrCreateSection<WeatherConfigurationSection>("weather", init =>
            {
                init.Token = "";
                init.CityId = 0;
                init.TimeZone = 3;
            });
        }

        public string Token => _section.Token;

        public int CityId => _section.CityId;

        public int TimeZone => _section.TimeZone;
    }
}
