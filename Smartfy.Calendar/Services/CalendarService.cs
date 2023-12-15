using Microsoft.Extensions.Logging;
using Smartfy.Calendar.Entity;
using Smartfy.Calendar.Utils;
using Smartfy.Core.Messages.Strategies.Utils;
using Smartfy.Core.Services;
using System.Collections;

namespace Smartfy.Calendar.Services
{
    internal class CalendarService : ICalendarService
    {
        private IDayRepository _repository;
        private ILogger<CalendarService> _logger;
        private IServiceCollection _services;

        public CalendarService(IDayRepository repository, ILogger<CalendarService> logger, IServiceCollection services)
        {
            _repository = repository;
            _logger = logger;
            _services = services;
        }

        public virtual ICalendarDayFactory GetCalendarDayFactory()
        {
            return new CalendarDayFactory();
        }

        public CalendarDay[] GetCalendarDaysForDate(DateTime date)
        {
            var result = new List<CalendarDay>();
            
            foreach (var day in _repository.GetAll())
            {
                var calendarDay = GetCalendarDayFactory().Create(day);

                if (calendarDay.IsDay(date))
                {
                    result.Add(calendarDay);
                }
            }

            return result.ToArray();
        }
    }
}
