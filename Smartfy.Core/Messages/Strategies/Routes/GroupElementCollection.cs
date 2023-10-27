using System.Configuration;

namespace Smartfy.Core.Messages.Strategies.Utils
{
    public class GroupElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GroupElement) element).Group;
        }

        public void Add(GroupElement element)
        {
            BaseAdd(element);
        }

        public GroupElement GetGroup(string group)
        {
            return (GroupElement)this.BaseGet(group);
        }

    }
}