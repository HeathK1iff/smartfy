using System.Configuration;

namespace Smartfy.Calendar.Configuration.Impl
{
    internal class CalendarConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("public_calendar")]
        public string PublicCalendarFileName
        {
            get
            {
                return this["public_calendar"] as string ?? string.Empty;
            }
            set
            {
                this["public_calendar"] = value;
            }
        }
    }
}
