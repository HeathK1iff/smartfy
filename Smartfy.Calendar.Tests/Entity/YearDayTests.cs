using NUnit.Framework;
using Smartfy.Calendar.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartfy.Calendar.Entity.Tests
{
    [TestFixture()]
    public class YearDayTests
    {
        [Test()]
        public void IsDay_DateArgInPast_FromTodayShouldBeFalse()
        {
            YearDay day = new YearDay(10, 22, "My description", TypeOfDayEnum.Birthday);
            
            Assert.IsFalse(day.IsDay(new DateTime(2023, 10, 21)));
        }

        [Test()]
        public void IsDay_DateArgInFutureFromToday_ShouldBeFalse()
        {
            YearDay day = new YearDay(10, 22, "My description", TypeOfDayEnum.Birthday);

            Assert.IsFalse(day.IsDay(new DateTime(2023, 10, 21)));
        }

        [Test()]
        public void IsDay_DateArgTodayFromToday_ShouldBeTrue()
        {
            DateTime today = DateTime.Today;
            YearDay day = new YearDay(today.Month, today.Day, "My description", TypeOfDayEnum.Birthday);

            Assert.IsTrue(day.IsDay(today));
        }
    }
}