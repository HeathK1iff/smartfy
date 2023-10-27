using System.Configuration;

namespace Smartfy.Education.Configuration.Impl
{
    internal class EducationConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("url")]
        public string Url
        {
            get
            {
                return this["url"] as string ?? string.Empty;
            }
            set
            {
                this["url"] = value;
            }
        }

        [ConfigurationProperty("userName")]
        public string UserName
        {
            get
            {
                return this["userName"] as string ?? string.Empty;
            }
            set
            {
                this["userName"] = value;
            }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get
            {
                return this["password"] as string ?? string.Empty;
            }
            set
            {
                this["password"] = value;
            }
        }

    }
}
