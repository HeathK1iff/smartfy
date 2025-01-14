using System.Configuration;

namespace Smartfy.Core.Messages.Strategies.Utils
{
    public class RoutesConfiguration: ConfigurationSection
    {
        [ConfigurationProperty("groups", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(GroupElementCollection))]
        public GroupElementCollection Groups
        {
            get
            {
                return (GroupElementCollection)this["groups"];
            }
            set
            {
                this["groups"] = value;
            }
        }
    }
}