using Smartfy.Core.Entities;

namespace Smartfy.Core.Messages
{
    internal interface IMessageBroker
    {
        void Publish<T>(T message) where T : Message;

        IDisposable Subscribe<T>(IMessageSubscriber subscriber) where T : Message;
    }
}
