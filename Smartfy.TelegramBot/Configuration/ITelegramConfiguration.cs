namespace Smartfy.TelegramBot.Configuration
{
    public interface ITelegramConfiguration
    {
        string Token { get; }

        string SessionsPath { get; }    
    }
}