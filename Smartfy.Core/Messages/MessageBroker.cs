using Smartfy.Core.Entities;
using Smartfy.Core.Messages.Strategies;
using Smartfy.Core.Utils;

namespace Smartfy.Core.Messages
{
    public class MessageBroker : IMessageBroker
    {
        private Dictionary<Type, List<IMessageSubscriber>> _subscribers = new();
        private Dictionary<Type, IPublishStrategy> _strategy = new();
        private readonly IPublishStrategy _defaultStrategy = new PublishAllStrategy();

        private IPublishStrategy GetPublishStrategy(Type type)
        {
            if (_strategy.TryGetValue(type, out var strategy))
            {
                return strategy;
            }

            return _defaultStrategy;
        }

        public void AddPublishStrategy<T>(IPublishStrategy strategy) where T : Message
        {
            if (!_strategy.ContainsKey(typeof(T)))
            {
                _strategy.Add(typeof(T), strategy);
            }
        }

        public void Publish<T>(T message) where T : Message
        {
            if (message is null)
            {
                throw new ArgumentNullException("Message shoud not empty or null");
            }

            var strategy = GetPublishStrategy(typeof(T));
            strategy.PublishAll(message, _subscribers);
        }

        public IDisposable Subscribe<T>(IMessageSubscriber subscriber) where T : Message
        {
            if (!_subscribers.TryGetValue(typeof(T), out var list))
            {
                list = new List<IMessageSubscriber>();
                _subscribers.Add(typeof(T), list);
            }

            list.Add(subscriber);
            return new Unsubscriber(list, subscriber);
        }

        class Unsubscriber : IDisposable
        {
            private List<IMessageSubscriber> _list;
            private IMessageSubscriber _item;
            public Unsubscriber(List<IMessageSubscriber> list, IMessageSubscriber item)
            {
                _item = item;
                _list = list;
            }

            public void Dispose()
            {
                _list.Remove(_item);
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                _item = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            }
        }
    }
}
