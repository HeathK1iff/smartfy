using Moq;
using NUnit.Framework;
using Smartfy.Core.Entities;

namespace Smartfy.Core.Messages.Strategies.Tests
{
    [TestFixture()]
    public class PublishAllStrategyTests
    {
        [Test()]
        public void PublishAll_CheckRecieveMessageAllSubscribers_MessageShouldBeReceived()
        {
            var subscriber = new Mock<IMessageSubscriber>();
            subscriber.Setup(f => f.OnReceived(It.IsAny<Message>())).Verifiable();
            var subscriber2 = new Mock<IMessageSubscriber>();
            subscriber2.Setup(f => f.OnReceived(It.IsAny<Message>())).Verifiable();
            var subscribers = new Dictionary<Type, List<IMessageSubscriber>>()
            {
                { typeof(IncomeMessage), new List<IMessageSubscriber>()
                    {
                        subscriber.Object,
                        subscriber2.Object
                    }
                }
            };
            var message = new IncomeMessage()
            {
                Sender = "User",
                Data = "Test"
            };
            var sut = new PublishAllStrategy();

            sut.PublishAll(message, subscribers);

            subscriber.Verify(f => f.OnReceived(It.IsAny<Message>()));
            subscriber2.Verify(f => f.OnReceived(It.IsAny<Message>()));
        }

        [Test()]
        public void PublishAll_CheckIssueWithNullMessageArgument_ThrowArgumentNullException()
        {
            var subscriber = new Mock<IMessageSubscriber>();
            subscriber.Setup(f => f.OnReceived(It.IsAny<Message>())).Verifiable();
            var subscribers = new Dictionary<Type, List<IMessageSubscriber>>()
            {
                { typeof(IncomeMessage), new List<IMessageSubscriber>()
                    {
                        subscriber.Object
                    }
                }
            };

            var sut = new PublishAllStrategy();

            Assert.Throws<ArgumentNullException>(() => sut.PublishAll(null, subscribers));
        }

        [Test()]
        public void PublishAll_CheckIssueWithNullListOfSusbcribersArgument_ThrowArgumentNullException()
        {
            var message = new IncomeMessage()
            {
                Sender = "User",
                Data = "Test"
            };
            var sut = new PublishAllStrategy();

            Assert.Throws<ArgumentNullException>(() => sut.PublishAll(message, null));
        }
    }
}