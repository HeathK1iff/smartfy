using System.Configuration;

namespace Smartfy.Core.Utils
{
    public static class ConfigurationExtension
    {
        public static T GetOrCreateSection<T>(this Configuration configuration, string name, Action<T> initial) where T : ConfigurationSection
        {
            if (configuration.GetSection(name) is null)
            {
                var section = Activator.CreateInstance(typeof(T)) as T;
                configuration.Sections.Add(name, section);

                if (section != null)
                    initial?.Invoke(section);

                configuration.Save(ConfigurationSaveMode.Modified);
            }

            return (T)configuration.GetSection(name);
        }
    }
}
