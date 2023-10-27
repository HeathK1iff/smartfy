using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Core.Services.Messages;
using Smartfy.TelegramBot.Configuration.Adapter;
using Smartfy.TelegramBot.Services;
using System.Configuration;

namespace Smartfy.TelegramBot
{
    public sealed class LibraryLoader
    {
        public static void CreateService(System.Configuration.Configuration configuration, 
            ILoggerFactory loggerFactory, 
            IServiceCollection services)
        {
            services.AddService<ITelegramService>(new TelegramService(new TelegramConfigurationAdapter(configuration), loggerFactory.CreateLogger<TelegramService>(), 
                services.GetService<IMessageService>()));
        }
    }
}
