using System.Configuration;

namespace Smartfy.Core.Messages.Strategies.Utils
{
    public class GroupElement : ConfigurationElement
    {
        private List<string> _recepients = new();
        
        [ConfigurationProperty("group", IsRequired = true, IsKey = true)]
        public string Group
        {
            get
            {
                return (string)this["group"];
            }
            set
            {
                this["group"] = value;
            }
        }

        [ConfigurationProperty("recepients", IsRequired = true)]
        public string Recepients {
            get
            {
                return (string) this["recepients"];
            }
            set
            {
                this["recepients"] = value;
            }
        }
    }
}