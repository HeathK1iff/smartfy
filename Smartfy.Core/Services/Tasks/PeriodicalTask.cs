using Microsoft.Extensions.Logging;
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
        private ILogger _logger;

        public PeriodicalTask(int hour, int minute, ITask task, ILogger logger) : this(hour, minute,
            AllWeekDays, task, logger)
        {

        }

        public PeriodicalTask(int hour, int minute, DayOfWeek[] dayOfWeeks, ITask task, ILogger logger)
        {
            _logger = logger;

            var now = DateTime.Now;
            _executeDate = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
            if (_executeDate < now)
            {
                _executeDate = _executeDate.AddDays(1);
            }
            _logger.LogDebug($"Calculated time for execute task: {_executeDate}");
            _dayOfWeeks = dayOfWeeks;

            _task = task;
        }

        public bool Exeсute()
        {
            DateTime now = DateTime.Now;
            if ((now < _executeDate) && (_dayOfWeeks.Any(f => f == now.DayOfWeek)))
                return false;

            _task.Exeсute();
            _logger.LogDebug("Periodical task executed");
            _executeDate = _executeDate.AddDays(1);
            _logger.LogDebug($"Recalculated time for execute task: {_executeDate}");
            return false;
        }
    }
}
