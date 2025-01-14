using Smartfy.Core.Entities;

namespace Smartfy.Core.Messages.Strategies
{
    public class PublishAllStrategy : IPublishStrategy
    {
        public void PublishAll(Message message, Dictionary<Type, List<IMessageSubscriber>> subscribers)
        {
            if (message is null)
            {
                throw new ArgumentNullException($"{nameof(message)} argument can not be null");
            }

            if (subscribers is null)
            {
                throw new ArgumentNullException($"{nameof(subscribers)} argument can not be null");
            }

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
