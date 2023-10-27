using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using Smartfy.Education.Services;
using Smartfy.Education.Configuration.Utils;

namespace Smartfy.TelegramBot
{
    public sealed class LibraryLoader
    {
        public static void CreateService(System.Configuration.Configuration configuration, 
            ILoggerFactory loggerFactory, 
            IServiceCollection services)
        {
            services.AddService<IEducationService>(new EducationService(new EducationConfigurationAdapter(configuration), 
                loggerFactory.CreateLogger<EducationService>(), services));
        }
    }
}
