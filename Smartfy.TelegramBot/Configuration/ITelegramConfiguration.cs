namespace Smartfy.TelegramBot.Configuration
{
    public interface ITelegramConfiguration
    {
        string Token { get; }

        ISessions Sessions { get; }

        void Save();
    }
}