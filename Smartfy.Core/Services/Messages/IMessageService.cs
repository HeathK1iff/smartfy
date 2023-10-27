using Smartfy.Core.Messages;
using Smartfy.Core.Messages.Strategies.Utils;

namespace Smartfy.Core.Services.Messages
{
    public interface IMessageService : IService, IMessageBroker
    {
        RouteCollection Routes { get; }
    }
}
