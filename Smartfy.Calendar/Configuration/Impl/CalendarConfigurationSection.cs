using System.Configuration;

namespace Smartfy.Calendar.Configuration.Impl
{
    internal class CalendarConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("calendar")]
        public string CalendarPath
        {
            get
            {
                return this["calendar"] as string ?? string.Empty;
            }
            set
            {
                this["calendar"] = value;
            }
        }
    }
}
