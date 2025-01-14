using Smartfy.Core.Entities;
using Smartfy.Core.Messages;
using Smartfy.Core.Messages.Strategies.Utils;

namespace Smartfy.Core.Services.Messages
{
    public interface IMessageService : IService
    {
        RouteCollection Routes { get; }

        void Publish<T>(T message) where T : Message;

        IDisposable Subscribe<T>(IMessageSubscriber subscriber) where T : Message;
    }
}
