using System;

namespace Smartfy.Core.Services.Tasks
{
    public sealed class PeriodicalTask : ITask
    {
        public static DayOfWeek[] AllWeekDays = {
            DayOfWeek.Sunday,
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday };
        public static DayOfWeek[] WorkWeekDays = {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday };
        public static DayOfWeek[] WeekendDays = {
            DayOfWeek.Sunday,
            DayOfWeek.Saturday };

        private ITask _task;
        private DayOfWeek[] _dayOfWeeks;
        private DateTime _executeDate;

        public PeriodicalTask(int hour, int minute, ITask task) : this(hour, minute,
            AllWeekDays, task)
        {

        }

        public PeriodicalTask(int hour, int minute, DayOfWeek[] dayOfWeeks, ITask task)
        {
            var now = DateTime.Now;
            _executeDate = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
            if (_executeDate < now)
            {
                _executeDate = _executeDate.AddDays(1);
            }
            _dayOfWeeks = dayOfWeeks;

            _task = task;
        }

        public bool Exeсute()
        {
            DateTime now = DateTime.Now;
            if ((now < _executeDate) && (_dayOfWeeks.Any(f => f == now.DayOfWeek)))
                return false;

            _task.Exeсute();
            _executeDate = _executeDate.AddDays(1);
            return false;
        }
    }
}
