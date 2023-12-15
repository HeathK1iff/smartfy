namespace Smartfy.Calendar.Entity
{
    public class YearDay : CalendarDay
    {
        private int _day;
        private int _month;

        public YearDay(int month, int day, string description, TypeOfDayEnum typeOfDay) : base(description, typeOfDay)
        { 
            _day = day;
            _month = month;
        }

        public override bool IsDay(DateTime date)
        {
            return (date.Day == _day) && (date.Month == _month);
        }
    }
}
