using Smartfy.Core.Entities;
using Smartfy.Core.Messages;
using Smartfy.Core.Messages.Strategies.Utils;
using Smartfy.Core.Messages.Strategies;
using System.Configuration;

namespace Smartfy.Core.Services.Messages
{
    public class MessageService : IMessageService
    {
        private Configuration _configuration;
        private MessageBroker _messageBroker;
        private RouteCollection _routes;
        public MessageService(Configuration configuration)
        {
            _configuration = configuration;
            _messageBroker = new MessageBroker();
            _routes = new RouteCollection(new RouteRepository(configuration));
            _messageBroker.AddPublishStrategy<GroupOutputMessage>(new PublishByGroupStrategy(_routes));
        }

        public RouteCollection Routes
        {
            get => _routes;
        }

        public void Publish<T>(T message) where T : Message
        {
            _messageBroker.Publish(message);
        }

        public IDisposable Subscribe<T>(IMessageSubscriber subscriber) where T : Message
        {
            return _messageBroker.Subscribe<T>(subscriber);
        }
    }
}
