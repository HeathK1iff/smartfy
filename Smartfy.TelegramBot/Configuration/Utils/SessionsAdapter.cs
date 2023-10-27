using Smartfy.TelegramBot.Configuration.Impl;

namespace Smartfy.TelegramBot.Configuration.Adapter
{
    internal class SessionsAdapter : ISessions
    {
        private SessionCollection _collection;
        public SessionsAdapter(SessionCollection collection)
        {
            _collection = collection;
        }

        public void Add(string userName, long chartId)
        {
            _collection.Add(new SessionElement()
            {
                UserName = userName,
                ChatId = chartId
            });
        }

        public bool TryGetChatId(string userName, out long? chatId)
        {
            chatId = null;
            var session = _collection.GetSessionByUserName(userName);
            if (session != null)
            {
                chatId = session.ChatId;
                return true;
            }
            return false;
        }
    }
}