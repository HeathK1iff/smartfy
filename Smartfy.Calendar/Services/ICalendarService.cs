using Smartfy.Calendar.Entity;
using Smartfy.Core.Services;

namespace Smartfy.Calendar.Services
{
    public interface ICalendarService : IService
    {
        public CalendarDay[] GetCalendarDaysForDate(DateTime date);
    }
}
