using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.Mqtt.Configuration.Utils;
using Smartfy.Mqtt.Services;

namespace Smartfy.Mqtt
{
    public sealed class LibraryLoader
    {
        public static void CreateService(System.Configuration.Configuration configuration, 
            ILoggerFactory loggerFactory, 
            IServiceCollection services)
        {
            services.AddService<IMqttService>(new MqttService(new MqttConfigurationAdapter(configuration), loggerFactory.CreateLogger<MqttService>(), 
                services.GetService<IMessageService>()));
        }
    }
}
