using Smartfy.Core.Entities;
using Smartfy.Core.Exceptions;
using Smartfy.Core.Messages.Strategies;

namespace Smartfy.Core.Messages
{
    internal class MessageBroker : IMessageBroker
    {
        private Dictionary<Type, List<IMessageSubscriber>> _subscribers = new();
        private Dictionary<Type, IPublishStrategy> _strategy = new();
        private readonly IPublishStrategy _defaultStrategy;

        public MessageBroker(IPublishStrategy defaultStrategy)
        {
            _defaultStrategy = defaultStrategy;
        }

        public MessageBroker() : this(new PublishAllStrategy())
        { 
        }

        private IPublishStrategy GetPublishStrategy(Type type)
        {
            if (GetStrategies().TryGetValue(type, out var strategy))
            {
                return strategy;
            }

            return _defaultStrategy;
        }

        public void AddPublishStrategy<T>(IPublishStrategy strategy) where T : Message
        {
            if (!GetStrategies().ContainsKey(typeof(T)))
            {
                GetStrategies().Add(typeof(T), strategy);
                return;
            }

            throw new StrategyAlreadyRegisteredException();
        }

        public void Publish<T>(T message) where T : Message
        {
            if (message is null)
            {
                throw new ArgumentNullException("Message shoud not empty or null");
            }

            var strategy = GetPublishStrategy(typeof(T));
            strategy.PublishAll(message, GetSubscribers());
        }

        public IDisposable Subscribe<T>(IMessageSubscriber subscriber) where T : Message
        {
            if (subscriber is null)
            {
                throw new ArgumentNullException("Subscriber should not empty or null");
            }

            if (!GetSubscribers().TryGetValue(typeof(T), out var list))
            {
                list = new List<IMessageSubscriber>();
                GetSubscribers().Add(typeof(T), list);
            }

            list.Add(subscriber);

            return new Unsubscriber(typeof(T), (type, item) => {
                RemoveSubscriber(type, item);
            }, subscriber);
        }

        private void RemoveSubscriber(System.Type type, IMessageSubscriber subscriber)
        {
            if (GetSubscribers().TryGetValue(type, out var list))
            {
                list.Remove(subscriber);

                if (list.Count == 0)
                {
                    GetSubscribers().Remove(type);
                }
            }
        }

        protected Dictionary<Type, IPublishStrategy> GetStrategies()
        {
            return _strategy;
        }

        protected Dictionary<Type, List<IMessageSubscriber>> GetSubscribers()
        {
            return _subscribers;
        }

        class Unsubscriber : IDisposable
        {
            private System.Type _type;
            private Action<System.Type, IMessageSubscriber> _deleteAction;
            private IMessageSubscriber _item;
            public Unsubscriber(System.Type type, Action<System.Type, IMessageSubscriber> deleteAction, IMessageSubscriber item)
            {
                _type = type;
                _deleteAction = deleteAction;
                _item = item;
            }

            public void Dispose()
            {
                _deleteAction(_type, _item);
                _item = null;
            }
        }
    }
}
