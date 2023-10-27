using Moq;
using NUnit.Framework;
using Smartfy.Core;
using Smartfy.Core.Entities;
using Smartfy.Core.Messages;
using Smartfy.Core.Messages.Strategies;
using Smartfy.Core.Messages.Strategies.Utils;

namespace Smartfy.Core.Tests
{
    [TestFixture()]
    public class MessageBrokerTests
    {
        [Test()]
        public void Publish_OneSubscriber_ReceivedMessage()
        {
            //var routes = new RouteCollection()
            //{
            //    new Route()
            //    {
            //        Group = "log",
            //        Recepient = "test"
            //    }
            //};
            //var broker = new MessageBroker();
            //var subscriber = new Mock<IMessageSubscriber>();
            //broker.AddPublishStrategy<GroupMessage>(new PublishByGroupStrategy(routes));
            //broker.Subscribe<GroupMessage>(subscriber.Object);


            //broker.Publish<GroupMessage>(new GroupMessage()
            //{
            //    RecepientGroups = new string[]
            //    {
            //        "log"
            //    },
            //    Data = "Hello World"
            //});


            //subscriber.Verify(f => f.OnReceived(It.IsAny<Message>()), Times.Once);
        }

        [Test()]
        public void Publish_MultiplySubscriber_AllShouldReceived()
        {
            //var routes = new Messages.Strategies.Utils.RouteList()
            //{
            //    new Route()
            //    {
            //        Group = "log",
            //        Recepient = "test"
            //    }
            //};
            //var broker = new MessageBroker();
            //var subscriber = new Mock<IMessageSubscriber>();
            //var subscriber2 = new Mock<IMessageSubscriber>();
            //broker.AddPublishStrategy<GroupMessage>(new PublishByGroupStrategy(routes));
            //broker.Subscribe<GroupMessage>(subscriber.Object);
            //broker.Subscribe<GroupMessage>(subscriber2.Object);

            //broker.Publish<GroupMessage>(new GroupMessage()
            //{
            //    RecepientGroups = new string[]
            //    {
            //        "log"
            //    },
            //    Data = "Hello World"
            //});


            //subscriber.Verify(f => f.OnReceived(It.IsAny<Message>()), Times.Once);
            //subscriber2.Verify(f => f.OnReceived(It.IsAny<Message>()), Times.Once);
        }

        [Test()]
        public void Publish_OneSubscriber3()
        {
            //var routes = new Messages.Strategies.Utils.RouteList()
            //{
            //    new Route()
            //    {
            //        Group = "log",
            //        Recepient = "test"
            //    }
            //};
            //var broker = new MessageBroker();
            //var subscriber = new Mock<IMessageSubscriber>();
            //var subscriber2 = new Mock<IMessageSubscriber>();
            //broker.AddPublishStrategy<GroupMessage>(new PublishByGroupStrategy(routes));
            //broker.Subscribe<GroupMessage>(subscriber.Object);
            //broker.Subscribe<GroupMessage>(subscriber2.Object);

            //broker.Publish<GroupMessage>(new GroupMessage()
            //{
            //    RecepientGroups = new string[]
            //    {
            //        "logX"
            //    },
            //    Data = "Hello World"
            //});


            //subscriber.Verify(f => f.OnReceived(It.IsAny<Message>()), Times.Never);
            //subscriber2.Verify(f => f.OnReceived(It.IsAny<Message>()), Times.Never);
        }
    }
}