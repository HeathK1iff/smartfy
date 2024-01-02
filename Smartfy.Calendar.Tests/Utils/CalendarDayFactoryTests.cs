using NUnit.Framework;
using Smartfy.Calendar.Entity;
using Smartfy.Calendar.Exception;

namespace Smartfy.Calendar.Utils.Tests
{
    [TestFixture()]
    public class CalendarDayFactoryTests
    {

        [Test()]
        public void Create_CreateFixedDay_ShouldBeFixedDay()
        {
            CalendarDayFactory factory = new CalendarDayFactory();
            var day = factory.Create(new Entity.CalendarDayDto()
            {
                Date = "30/04/2023",
                Description = "New Date",
                TypeOfDay = "birthday"
            });

            Assert.IsTrue(day is FixedDay);
        }

        [Test()]
        public void Create_CreateYearDay_ShouldBeYearDay()
        {
            CalendarDayFactory factory = new CalendarDayFactory();
            var day = factory.Create(new Entity.CalendarDayDto()
            {
                Date = "30/04/*",
                Description = "New Date",
                TypeOfDay = "birthday"
            });

            Assert.IsTrue(day is YearDay);
        }

        [Test()]
        public void Create_CreateMonthDay_ShouldBeMonthDay()
        {
            CalendarDayFactory factory = new CalendarDayFactory();
            var day = factory.Create(new Entity.CalendarDayDto()
            {
                Date = "30/*/*",
                Description = "New Date",
                TypeOfDay = "birthday"
            });

            Assert.IsTrue(day is MonthDay);
        }


        [TestCase("999/999/999")]
        [TestCase("99/999/999")]
        [TestCase("SomeValue")]
        [TestCase("99/99/999")]
        public void Create_CreateWithIncorrectDateFormat_ThrowArgumentParceException(string date)
        {
            CalendarDayFactory factory = new CalendarDayFactory();
            var day = new Entity.CalendarDayDto()
            {
                Date = date,
                Description = "New Date",
                TypeOfDay = "birthday"
            };

            Assert.Throws<ArgumentParceException>(() => factory.Create(day));
        }

        [TestCase("birthday")]
        [TestCase("family-date")]
        [TestCase("event")]
        [TestCase("payment-date")]
        public void Create_CheckTypeOfDayPossibleValues_ShouldBeSuccessParced(string typeOfDay)
        {
            CalendarDayFactory factory = new CalendarDayFactory();
            var day = new Entity.CalendarDayDto()
            {
                Date = "12/12/2023",
                Description = "New Date",
                TypeOfDay = typeOfDay
            };

            var createdDay = factory.Create(day);

            Assert.IsTrue(createdDay is FixedDay);
            Assert.IsTrue(createdDay.TypeOfDay == ConvertToTypeOfDayEnum(typeOfDay));
        }

        [TestCase("BirthDay")]
        [TestCase("Family-Date")]
        [TestCase("EvEnt")]
        [TestCase("Payment-date")]
        public void Create_CheckTypeOfDayPossibleCaseInsensitiveValues_ShouldBeSuccessParced(string typeOfDay)
        {
            CalendarDayFactory factory = new CalendarDayFactory();
            var day = new Entity.CalendarDayDto()
            {
                Date = "12/12/2023",
                Description = "New Date",
                TypeOfDay = typeOfDay
            };

            var createdDay = factory.Create(day);

            Assert.IsTrue(createdDay is FixedDay);
            Assert.IsTrue(createdDay.TypeOfDay == ConvertToTypeOfDayEnum(typeOfDay));
        }

        public TypeOfDayEnum ConvertToTypeOfDayEnum(string type)
        {
            switch (type.ToLower().Trim())
            {
                case "birthday": return TypeOfDayEnum.Birthday;
                case "family-date": return TypeOfDayEnum.FamilyDate;
                case "event": return TypeOfDayEnum.Event;
                case "public-holiday": return TypeOfDayEnum.Holiday;
                case "payment-date": return TypeOfDayEnum.PaymentDate;
            }

            throw new ArgumentParceException(type);
        }
    }
}

