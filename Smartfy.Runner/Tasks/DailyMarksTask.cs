using Microsoft.Extensions.Logging;
using Smartfy.Core.Entities;
using Smartfy.Core.Messages.Strategies.Utils;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Core.Services.Tasks;
using Smartfy.Education.Services;
using System.Text;

namespace Smartfy.Runner.Tasks
{
    internal class DailyMarksTask : ITask
    {
        private static string EducationRecepientGroup = "education";
        private IServiceCollection _services;
        private ILogger _logger;

        public DailyMarksTask(IServiceCollection services, ILogger logger)
        {
            _services = services;
            _logger = logger;
        }

        public bool Exeсute()
        {
            var messageService = _services.GetService<IMessageService>();
            var dailyMarks = _services.GetService<IEducationService>().GetDailyMarks();

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("🏫 Отчет по школе:");
            foreach (var mark in dailyMarks)
            {
                messageBuilder.AppendLine($"{mark.Subject} : {mark.Mark}");
            }

            _services.GetService<IMessageService>().Routes.AddRouteIfNotExist(new Route()
            {
                Group = EducationRecepientGroup,
                Recepient = ""
            });

            var success = dailyMarks.Count > 0;

            if (success)
            {
                _logger.LogInformation($"Found {dailyMarks.Count} daily marks");
                messageService.Publish(new GroupOutputMessage()
                {
                    Data = messageBuilder.ToString(),
                    RecepientGroups = new string[] { EducationRecepientGroup }
                });
            }
            _logger.LogDebug($"Daily marks is not found for today");
            
            return true;
        }
    }
}
