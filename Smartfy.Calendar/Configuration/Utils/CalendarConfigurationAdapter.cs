
using static System.Collections.Specialized.BitVector32;
using System.Configuration;
using Smartfy.Calendar.Configuration.Impl;
using Smartfy.Core.Utils;

namespace Smartfy.Calendar.Configuration.Utils
{
    internal class CalendarConfigurationAdapter : ICalendarConfiguration
    {
        private readonly CalendarConfigurationSection _section;

        public CalendarConfigurationAdapter(System.Configuration.Configuration configuration)
        { 
            _section = configuration.GetOrCreateSection<CalendarConfigurationSection>("calendar", init =>
            {
                init.PublicCalendarFileName = "public_calendar.json";
            });
        }

        public string PublicCalendarFileName
        {
            get => _section.PublicCalendarFileName;
            set
            {
                _section.PublicCalendarFileName = value;
            }
        }
    }
}
