using Microsoft.Extensions.Logging;
using Smartfy.Calendar.Entity;
using System.Text.Json;

namespace Smartfy.Calendar.Utils
{
    internal class JsonDayRepository : IDayRepository
    {
        private readonly string _fileNamePath;
        private readonly ILogger _logger;
        public JsonDayRepository(string fileNamePath, ILogger<JsonDayRepository> logger)
        {
            _fileNamePath = fileNamePath;
            _logger = logger;
        }

        public CalendarDayDto[] GetAll()
        {
            if (!File.Exists(_fileNamePath))
            {
                _logger.LogWarning($"File {_fileNamePath} is not found");

                var jsonText = JsonSerializer.Serialize(new CalendarDayDto[1]
                {
                    new CalendarDayDto
                    {
                        Date = "00/00/0000",
                        Description = string.Empty,
                        TypeOfDay = string.Empty
                    }
                });

                File.WriteAllText(_fileNamePath, jsonText);
                _logger.LogWarning($"File: {_fileNamePath} is created with default calendar day");
            }

            var jsonData = File.ReadAllText(_fileNamePath);
            var list = JsonSerializer.Deserialize<CalendarDayDto[]>(jsonData)?.Where(f => !f.Date.Equals("00/00/0000")).ToArray() ?? new CalendarDayDto[0];

            _logger.LogInformation($"Loaded {list.Length} days from calendar file: {_fileNamePath}");
            return list;
        }
    }
}
