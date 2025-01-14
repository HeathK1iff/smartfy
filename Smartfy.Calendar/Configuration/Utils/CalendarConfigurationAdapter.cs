
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
            string basefolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration");

            if (!Directory.Exists(basefolder))
            {
                Directory.CreateDirectory(basefolder);
            }

            _section = configuration.GetOrCreateSection<CalendarConfigurationSection>("calendar", init =>
            {
                init.CalendarPath = Path.Combine(basefolder, "calendar.json");
            });
        }

        public string CalendarPath
        {
            get => _section.CalendarPath;
            set
            {
                _section.CalendarPath = value;
            }
        }
    }
}
