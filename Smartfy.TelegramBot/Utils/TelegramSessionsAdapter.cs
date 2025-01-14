namespace Smartfy.TelegramBot.Classes
{
    public class TelegramSessionsAdapter : ITelegramSessions
    {
        TelegramSessionRepository _repository;
        public TelegramSessionsAdapter(TelegramSessionRepository repository)
        {
            _repository = repository;
        }

        public TelegramSession[] GetAll()
        {
            return _repository.GetAll();
        }
    }
}
