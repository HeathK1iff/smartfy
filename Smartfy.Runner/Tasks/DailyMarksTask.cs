using EduRk.Entity;
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
        private const string EducationRecepientGroup = "education";
        private ILogger? _logger;
        public void Exeсute(IServiceCollection services, ref bool success)
        {
            var messageService = services.GetService<IMessageService>();
            var dailyMarks = services.GetService<IEducationService>().GetDailyMarks();

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("Отчет по школе:");
            foreach (var mark in dailyMarks)
            {
                messageBuilder.AppendLine($"{mark.Subject} : {mark.Mark}");
            }

            services.GetService<IMessageService>().Routes.AddRouteIfNotExist(new Route()
            {
                Group = EducationRecepientGroup,
                Recepient = ""
            });

            success = dailyMarks.Count > 0;

            if (success)
            {
                _logger?.LogInformation($"Found {dailyMarks.Count} daily marks");
                messageService.Publish(new GroupOutputMessage()
                {
                    Data = messageBuilder.ToString(),
                    RecepientGroups = new string[] { EducationRecepientGroup }
                });
            }
        }

        public bool Prepare(System.Action? executeAction, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DailyMarksTask>();
            return true;
        }
    }
}
