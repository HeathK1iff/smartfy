namespace Smartfy.Calendar.Entity
{
    public enum TypeOfDayEnum { Birthday, FamilyDate, Event, Holiday, PaymentDate }
    public abstract class CalendarDay
    {
        public CalendarDay(string description, TypeOfDayEnum typeOfDay)
        {
            Description = description;
            TypeOfDay = typeOfDay;
        }

        public abstract bool IsDay(DateTime date);
        public string Description { get; }
        public TypeOfDayEnum TypeOfDay { get; }
    }
}
