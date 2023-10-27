using Smartfy.Core.Entities;
using Smartfy.Core.Messages;

namespace Smartfy.Core.Messages.Strategies
{
    public class PublishAllStrategy : IPublishStrategy
    {
        public void PublishAll(Message message, Dictionary<Type, List<IMessageSubscriber>> subscribers)
        {
            if (subscribers.TryGetValue(message.GetType(), out var list))
            {
                foreach (var subscriber in list)
                {
                    subscriber.OnReceived(message);
                }
            }
        }
    }
}
