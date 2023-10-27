using Smartfy.Core.Entities;

namespace Smartfy.Core.Messages
{
    public interface IPublishStrategy
    {
        void PublishAll(Message message, Dictionary<Type, List<IMessageSubscriber>> subscribers);
    }
}
