using System.Configuration;

namespace Smartfy.TelegramBot.Configuration.Impl
{
    internal class SessionElement : ConfigurationElement
    {
        [ConfigurationProperty("userName", IsRequired = true, IsKey = true)]
        public string UserName
        {
            get
            {
                return (string)this["userName"];
            }
            set
            {
                this["userName"] = value;
            }
        }

        [ConfigurationProperty("chatId")]
        public long ChatId
        {
            get
            {
                return (long)this["chatId"];
            }
            set
            {
                this["chatId"] = value;
            }
        }
    }
}