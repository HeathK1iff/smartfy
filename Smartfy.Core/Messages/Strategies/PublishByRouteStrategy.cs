using Smartfy.Core.Entities;
using Smartfy.Core.Messages.Strategies.Utils;

namespace Smartfy.Core.Messages.Strategies
{
    public class PublishByRouteStrategy : IPublishStrategy
    {
        private readonly RouteCollection _routes;

        public PublishByRouteStrategy(RouteCollection routes)
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
                var routes = _routes.GetAll().Where(f => f.Group.Equals(group, StringComparison.InvariantCultureIgnoreCase));
                if (!routes.Any())
                    continue;

                foreach (var subscriber in outSubscribers)
                {
                    foreach (var route in routes)
                    {
                        if (string.IsNullOrEmpty(route.Recepient))
                            continue;

                        subscriber.OnReceived(new OutputMessage()
                        {
                            Recepient = route.Recepient,
                            Data = message.Data
                        });
                    }
                }
            }
        }
    }
}