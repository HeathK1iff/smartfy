using Microsoft.Extensions.Logging;
using Smartfy.Calendar.Configuration.Utils;
using Smartfy.Calendar.Services;
using Smartfy.Calendar.Utils;
using Smartfy.Core.Services;

namespace Smartfy.Calendar
{
    public sealed class LibraryLoader
    {
        public static void CreateService(System.Configuration.Configuration configuration,
            ILoggerFactory loggerFactory,
            IServiceCollection services)
        {
            var calendarConfiguration = new CalendarConfigurationAdapter(configuration);

            services.AddService<ICalendarService>(new CalendarService(new JsonDayRepository(calendarConfiguration.PublicCalendarFileName, loggerFactory.CreateLogger<JsonDayRepository>()), 
                loggerFactory.CreateLogger<CalendarService>(), services));
        }
    }
}