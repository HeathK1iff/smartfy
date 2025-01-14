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
    public class FixedDayTests
    {
        [Test()]
        public void IsDay_DateArgIsEqual_ShouldBeTrue()
        {
            CalendarDay sut = new FixedDay(2023, 12, 12, "My description", TypeOfDayEnum.Birthday);
            
            Assert.IsTrue(sut.IsDay(new DateTime(2023,12,12)));
        }

        [Test()]
        public void IsDay_DateArgIsNotEqual_ShouldBeFalse()
        {
            CalendarDay sut = new FixedDay(2023, 12, 12, "My description", TypeOfDayEnum.Birthday);

            Assert.IsFalse(sut.IsDay(new DateTime(2023, 12, 10)));
        }
    }
}