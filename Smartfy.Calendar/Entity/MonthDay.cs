namespace Smartfy.Calendar.Entity
{
    public class MonthDay : CalendarDay
    {
        private int _day;

        public MonthDay(int day, string description, TypeOfDayEnum typeOfDay) : base(description, typeOfDay)
        {
            _day = day;
        }

        public override bool IsDay(DateTime date)
        {
           return date.Day == _day;
        }

    }

}
