namespace Smartfy.TelegramBot.Configuration
{
    public interface ISessions
    {
        void Add(string userName, long chartId);
        bool TryGetChatId(string userName, out long? chatId);
    }
}