using EduRk;
using EduRk.Entity;
using EduRk.Exception;
using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Education.Configuration;
using Smartfy.Weather.Exceptions;
using System.Configuration;

namespace Smartfy.Education.Services
{
    public class EducationService : IEducationService
    {
        private readonly IServiceCollection _services;
        private readonly IEducationConfiguration _configuration;
        private readonly EduRkClient _client;
        private readonly ILogger _logger;

        public EducationService(IEducationConfiguration configuration, ILogger logger, IServiceCollection services)
        {
            _services = services;
            _configuration = configuration;

            if (string.IsNullOrWhiteSpace(_configuration.Url))
            {
                throw new InvalidConfigurationException();
            }

            _client = new EduRkClient(configuration.Url);
            _logger = logger;
        }

        public List<(string Subject, string Mark)> GetDailyMarks()
        {
            var result = new List<(string Subject, string Mark)>();
            var isAuthoruzed = false;
            try
            {
                isAuthoruzed = _client.AutorizeAsync(_configuration.UserName, _configuration.Password).Result;
            } catch (NotAutorizedException ex)
            {
                _logger.LogWarning($"Authorization error: {ex.Message}");
            }

            if (!isAuthoruzed)
                return result;

            var marks = _client.GetDailyJournalAsync().Result ?? Array.Empty<DailyMark>();
            var todayMarks = marks.Where(m => m.Date >= DateTime.Today);

            foreach (var mark in todayMarks)
            {
                result.Add((Subject: mark.Subject.Name, Mark: mark.Value));
            }

            if (todayMarks.Count() > 0)
            {
                _logger.LogInformation($"Found {todayMarks.Count()} marks for today");
            }

            return result;
        }
    }
}
