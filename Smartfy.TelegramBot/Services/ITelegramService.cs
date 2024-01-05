using Smartfy.Core.Services;
using Smartfy.TelegramBot.Classes;

namespace Smartfy.TelegramBot.Services
{
    public interface ITelegramService : IService 
    { 
        public ITelegramSessions Sessions { get; }
    
    }
}