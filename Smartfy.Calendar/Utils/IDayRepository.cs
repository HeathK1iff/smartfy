using Smartfy.Calendar.Entity;

namespace Smartfy.Calendar.Utils
{
    internal interface IDayRepository
    {
        CalendarDayDto[] GetAll();
    }
}
