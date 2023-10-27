using System.Configuration;

namespace Smartfy.TelegramBot.Configuration.Impl
{
    internal class SessionCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            ;
            return new SessionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as SessionElement).UserName;
        }

        public SessionElement? GetSessionByUserName(string userName)
        {
            foreach (SessionElement item in this)
            {
                if (item.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return item;
                }
            }

            return default;
        }

        public void Add(SessionElement element)
        {
            BaseAdd(element);
        }
    }
}