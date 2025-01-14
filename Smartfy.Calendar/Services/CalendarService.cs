using Microsoft.Extensions.Logging;
using Smartfy.Calendar.Entity;
using Smartfy.Calendar.Exception;
using Smartfy.Calendar.Utils;
using Smartfy.Core.Services;

namespace Smartfy.Calendar.Services
{
    internal class CalendarService : ICalendarService
    {
        private IDayRepository _repository;
        private ILogger<CalendarService> _logger;
        private IServiceCollection _services;
        private List<CalendarDay> _days = new List<CalendarDay>();

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


        public void Refresh()
        {
            _days.Clear();
            var days = _repository.GetAll();
            foreach (var day in days)
            {
                try
                {
                    var calendarDay = GetCalendarDayFactory().Create(day);
                    _days.Add(calendarDay);
                }
                catch (ArgumentParceException e)
                {
                    _logger.LogError(e.Message, e);
                }
            }
            _logger.LogInformation($"Calendar is loaded {days.Length}");
        }


        public CalendarDay[] GetCalendarDaysForDate(DateTime date)
        {
            if (_days.Count == 0)
            {
                Refresh();
            }

            return _days.Where(f => f.IsDay(date)).ToArray();
        }
    }
}
