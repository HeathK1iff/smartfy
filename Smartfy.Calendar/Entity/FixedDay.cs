
namespace Smartfy.Calendar.Entity
{
    public class FixedDay : CalendarDay
    {
        private DateTime _date; 

        public FixedDay(int year, int month, int day, string description, TypeOfDayEnum typeOfDay) : base(description, typeOfDay)
        {
            _date = new DateTime(year, month, day);
        }


        public override bool IsDay(DateTime date)
        {
            return _date.Equals(date);
        }
    }
}
