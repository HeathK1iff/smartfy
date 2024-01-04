using Microsoft.Extensions.Logging;
using Smartfy.Core.Entities;
using Smartfy.Core.Messages.Strategies.Utils;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Core.Services.Tasks;
using Smartfy.Device.Entity;
using Smartfy.Device.Service;
using Smartfy.DomainModel.Tasks.Utils;

namespace Smartfy.Runner.Tasks
{
    public class SensorTask : ITask
    {
        private IDeviceService _deviceService;
        private IMessageService _messageService;
        private ILogger _logger;
        public SensorTask(IServiceCollection services, ILogger logger) 
        {
           _logger = logger;
           _deviceService = services.GetService<IDeviceService>();
           _messageService = services.GetService<IMessageService>();
        }

        public bool Exeсute()
        {
            var groupsByLocation = new MessageByGroupBuilder($"🏘 Отчет на {DateTime.Now.ToString("HH:mm")}");

            foreach (var device in _deviceService.GetAll())
            {
                if (string.IsNullOrWhiteSpace(device.Location))
                    continue;

                var temp = device as ITemperature;

                if (temp != null)
                {
                    groupsByLocation.Add(device.Location, $"Температура: {temp.Temperature.GetValue()} °C");
                }

                var hum = device as IHumidity;
                if (hum != null)
                {
                    groupsByLocation.Add(device.Location, $"Влажность: {hum.Humidity.GetValue()} %");
                }

                var press = device as IPressure;
                if (press != null)
                {
                    groupsByLocation.Add(device.Location, $"Давление: {press.Pressure.GetValue()} гПа");
                }
            }

            _messageService.Routes.AddRouteIfNotExist(new Route()
            {
                Group = "climat",
                Recepient = ""
            });

            _messageService.Publish<GroupOutputMessage>(new GroupOutputMessage()
            {
                RecepientGroups = new string[] { "climat" },
                Data = groupsByLocation.ToString()
            });

            return true;
        }
    }
}
