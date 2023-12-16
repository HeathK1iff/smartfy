using Smartfy.Calendar.Entity;
using Smartfy.Calendar.Exception;
using System.Text.RegularExpressions;

namespace Smartfy.Calendar.Utils
{
    public class CalendarDayFactory : ICalendarDayFactory
    {
        private static string FixedDayRegexTemplate = @"^([0-9]{1,2})\/([0-9]{1,2})\/([2-9][0-9]{3})$";
        private static string YearDayRegexTemplate = @"^([0-9]{1,2})\/([0-9]{1,2})\/\*$";
        private static string MonthDayRegexTemplate = @"^([0-9]{1,2})\/\*\/\*$";

        public CalendarDayFactory()
        {

        }

        public CalendarDay Create(CalendarDayDto item)
        {
            if (Regex.IsMatch(item.Date, FixedDayRegexTemplate))
            {
                return CreateFixedDay(item);
            }
            else
            if (Regex.IsMatch(item.Date, YearDayRegexTemplate))
            {
                return CreateYearDay(item);
            }
            else
            if (Regex.IsMatch(item.Date, MonthDayRegexTemplate))
            {
                return CreateMonthDay(item);
            }

            throw new ArgumentParceException(nameof(item.Date));
        }

        private CalendarDay CreateFixedDay(CalendarDayDto item)
        {
            var match = Regex.Match(item.Date, FixedDayRegexTemplate);

            int day = int.Parse(match.Groups[1].Value);
            int month = int.Parse(match.Groups[2].Value);
            int year = int.Parse(match.Groups[3].Value);

            return new FixedDay(year, month, day, item.Description, ConvertToTypeOfDayEnum(item.TypeOfDay));
        }

        private CalendarDay CreateYearDay(CalendarDayDto item)
        {
            var match = Regex.Match(item.Date, YearDayRegexTemplate);
            int day = int.Parse(match.Groups[1].Value);
            int month = int.Parse(match.Groups[2].Value);

            return new YearDay(month, day, item.Description, ConvertToTypeOfDayEnum(item.TypeOfDay));
        }

        private CalendarDay CreateMonthDay(CalendarDayDto item)
        {
            var match = Regex.Match(item.Date, MonthDayRegexTemplate);
            int day = int.Parse(match.Groups[1].Value);

            return new MonthDay(day, item.Description, ConvertToTypeOfDayEnum(item.TypeOfDay));
        }

        public TypeOfDayEnum ConvertToTypeOfDayEnum(string type)
        {
            switch (type.ToLower().Trim())
            {
                case "birthday": return TypeOfDayEnum.Birthday;
                case "family-date": return TypeOfDayEnum.FamilyDate;
                case "event": return TypeOfDayEnum.Event;
                case "holiday": return TypeOfDayEnum.Holiday;
                case "payment-date": return TypeOfDayEnum.PaymentDate;
            }

            throw new ArgumentParceException($"The type ({type}) is not registered");
        }

    }
}
