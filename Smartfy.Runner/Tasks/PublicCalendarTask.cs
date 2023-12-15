using Microsoft.Extensions.Logging;
using Smartfy.Calendar.Entity;
using Smartfy.Calendar.Services;
using Smartfy.Core.Entities;
using Smartfy.Core.Messages.Strategies.Utils;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Core.Services.Tasks;
using System.Text;

namespace Smartfy.Runner.Tasks
{
    internal class PublicCalendarTask : ITask
    {
        private static string CalendarRecepientGroup = "calendar";
        private ILogger _logger;
        private IServiceCollection _services;

        public PublicCalendarTask(IServiceCollection services, ILogger logger)
        {
            _services = services;
            _logger = logger;
        }

        public bool Exeсute()
        {
            var messageService = _services.GetService<IMessageService>();
            var calendarDays = _services.GetService<ICalendarService>().GetCalendarDaysForDate(DateTime.Today);

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("Сегодня:");
            foreach (var day in calendarDays)
            {
                messageBuilder.AppendLine($"{TypeOfDayToSymbol(day.TypeOfDay)}: {day.Description}");
            }

            _services.GetService<IMessageService>().Routes.AddRouteIfNotExist(new Route()
            {
                Group = CalendarRecepientGroup,
                Recepient = ""
            });

            var success = calendarDays.Length > 0;

            if (success)
            {
                messageService.Publish(new GroupOutputMessage()
                {
                    Data = messageBuilder.ToString(),
                    RecepientGroups = new string[] { CalendarRecepientGroup }
                });

                return true;
            }
            _logger.LogDebug($"Events is not found for today");

            return true;
        }

        public string TypeOfDayToSymbol(TypeOfDayEnum typeOfDay)
        {
            switch (typeOfDay)
            {
                case TypeOfDayEnum.Birthday: return "🎂";
                case TypeOfDayEnum.FamilyDate: return "🎉";
                case TypeOfDayEnum.PaymentDate: return "💵";
                case TypeOfDayEnum.PublicHoliday: return "🏖";
                case TypeOfDayEnum.Event: return "🎇";
            }
            return string.Empty;
        }
    } 
}
