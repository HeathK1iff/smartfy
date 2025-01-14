using NUnit.Framework;

namespace Smartfy.Calendar.Entity.Tests
{
    [TestFixture()]
    public class MonthDayTests
    {
        [Test()]
        public void IsDay_DateArgInPast_ShouldBeFalse()
        {
            CalendarDay sut = new MonthDay(20, "My description", TypeOfDayEnum.Birthday);

            Assert.IsFalse(sut.IsDay(new DateTime(2023, 12, 18)));
        }

        [Test()]
        public void IsDay_DateArgInFuture_ShouldBeFalse()
        {
            CalendarDay sut = new MonthDay(20, "My description", TypeOfDayEnum.Birthday);

            Assert.IsFalse(sut.IsDay(new DateTime(2023, 12, 28)));
        }

        [Test()]
        public void IsDay_DateArgEqualCurrentMonthDate_ShouldBeTrue()
        {
            CalendarDay sut = new MonthDay(20, "My description", TypeOfDayEnum.Birthday);

            Assert.IsTrue(sut.IsDay(new DateTime(2023, 12, 20)));
        }
    }
}