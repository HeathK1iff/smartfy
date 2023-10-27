using System.Configuration;

namespace Smartfy.Weather.Configuration.Impl
{
    internal class WeatherConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("token")]
        public string Token
        {
            get
            {
                return this["token"] as string ?? string.Empty;
            }
            set
            {
                this["token"] = value;
            }
        }

        [ConfigurationProperty("cityId")]
        public int CityId
        {
            get
            {
                return (int)this["cityId"];
            }
            set
            {
                this["cityId"] = value;
            }
        }

        [ConfigurationProperty("timeZone")]
        public int TimeZone
        {
            get
            {
                return (int)this["timeZone"];
            }
            set
            {
                this["timeZone"] = value;
            }
        }

    }
}
