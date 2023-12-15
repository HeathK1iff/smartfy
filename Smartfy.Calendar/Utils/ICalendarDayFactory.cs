using Smartfy.Calendar.Entity;

namespace Smartfy.Calendar.Utils
{
    public interface ICalendarDayFactory
    {
        CalendarDay Create(CalendarDayDto item);
    }
}