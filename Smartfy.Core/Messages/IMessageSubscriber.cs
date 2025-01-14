using Smartfy.Core.Entities;

namespace Smartfy.Core.Messages
{
    public interface IMessageSubscriber
    {
        void OnReceived(Message message);
    }
}
