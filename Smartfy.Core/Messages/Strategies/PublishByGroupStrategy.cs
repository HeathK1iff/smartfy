using Smartfy.Core.Entities;
using Smartfy.Core.Messages.Strategies.Utils;

namespace Smartfy.Core.Messages.Strategies
{
    public class PublishByGroupStrategy : IPublishStrategy
    {
        private readonly RouteCollection _routes;

        public PublishByGroupStrategy(RouteCollection routes)
        {
            _routes = routes;
        }

        public void PublishAll(Message message, Dictionary<Type, List<IMessageSubscriber>> subscribers)
        {
            if (!(message is GroupOutputMessage))
            {
                return;
            }
            
            GroupOutputMessage? outMsg = message as GroupOutputMessage;

            if (!subscribers.TryGetValue(typeof(OutputMessage), out var outSubscribers))
            {
                return;
            }
            
            foreach (var group in outMsg?.RecepientGroups ?? Array.Empty<string>())
            {
                var recepients = _routes.GetAll().Where(f => f.Group.Equals(group, StringComparison.InvariantCultureIgnoreCase));
                if (!recepients.Any())
                    continue;

                foreach (var subscriber in outSubscribers)
                {
                    foreach (var recepient in recepients)
                    {
                        subscriber.OnReceived(new OutputMessage()
                        {
                            Recepient = recepient.Recepient,
                            Data = message.Data
                        });
                    }
                }
            }
        }
    }
}