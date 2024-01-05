namespace Smartfy.TelegramBot.Classes
{
    public interface ITelegramSessionRepository
    {
        void Add(TelegramSession session);

        bool TryGetChatId(string userName, out long? chatId);

        TelegramSession[] GetAll();
    }
}