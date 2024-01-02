using Smartfy.Core.Utils;
using Smartfy.Education.Configuration.Impl;

namespace Smartfy.Education.Configuration.Utils
{
    internal class EducationConfigurationAdapter : IEducationConfiguration
    {
        private readonly EducationConfigurationSection _section;
        public EducationConfigurationAdapter(System.Configuration.Configuration configuration) 
        {
            _section = configuration.GetOrCreateSection<EducationConfigurationSection>("education", init =>
            {
                init.Url = "";
                init.UserName  = "";
                init.Password = "";
            });
        }

        public string Url => _section.Url;

        public string UserName => _section.UserName;

        public string Password => _section.Password;
    }
}
