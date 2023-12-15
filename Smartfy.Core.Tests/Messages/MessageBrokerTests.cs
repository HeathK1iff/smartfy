using Smartfy.Core.Messages;
using Moq;
using NUnit.Framework;
using Smartfy.Core.Entities;
using Smartfy.Core.Exceptions;

namespace Smartfy.Core.Messages.Tests
{
    [TestFixture()]
    public class MessageBrokerTests
    {
        [Test()]
        public void AddPublishStrategy_CheckAppendToInternalList_ShouldBeOne()
        {
            var sut = new MessageBrokerExt();

            sut.AddPublishStrategy<Message>(new PublishStrategy());

            Assert.AreEqual(sut.GetStrategies().Count(), 1);
        }

        [Test()]
        public void AddPublishStrategy_CheckAddStrategyForExistingMessageType_ThrowStrategyAlreadyRegisteredException()
        {
            var sut = new MessageBrokerExt();
            sut.AddPublishStrategy<Message>(new PublishStrategy());
            Assert.Throws<StrategyAlreadyRegisteredException>(() => sut.AddPublishStrategy<Message>(new PublishStrategy()));
        }

        [Test()]
        public void Publish_PutNullMessage_ThrowException()
        {
            var sut = new MessageBroker();
            sut.AddPublishStrategy<Message>(new PublishStrategy());

            Assert.Throws<ArgumentNullException>(() => sut.Publish<Message>(null));
        }

        [Test()]
        public void Publish_CheckUseDefaultStrategy_ThrowException()
        {
            var strategy = new Mock<IPublishStrategy>();

            var sut = new MessageBroker(strategy.Object);

            strategy.Setup(f => f.PublishAll(It.IsAny<Message>(), It.IsAny<Dictionary<Type, List<IMessageSubscriber>>>())).Verifiable();

            sut.Publish<TestMessage>(new TestMessage() { Data = "" });

            strategy.VerifyAll();
        }

        [Test()]
        public void Publish_CheckUseOverrideStrategy_ThrowException()
        {
            var strategy = new Mock<IPublishStrategy>();

            var sut = new MessageBroker();

            sut.AddPublishStrategy<TestMessage>(strategy.Object);

            strategy.Setup(f => f.PublishAll(It.IsAny<Message>(), It.IsAny<Dictionary<Type, List<IMessageSubscriber>>>())).Verifiable();

            sut.Publish<TestMessage>(new TestMessage() { Data = "" });

            strategy.VerifyAll();
        }

        [Test()]
        public void Subscribe_CheckNullArgument_ThrowArgumentNullException()
        {
            var sut = new MessageBroker();

            Assert.Throws<ArgumentNullException>(() => sut.Subscribe<TestMessage>(null));
        }

        [Test()]
        public void Subscribe_CheckExistingSubscriber_SusbcriberShouldBeExisted()
        {
            var sut = new MessageBrokerExt();
            var subscriber = new Mock<IMessageSubscriber>();

            sut.Subscribe<TestMessage>(subscriber.Object);

            Assert.IsTrue(sut.GetSubscribers().Keys.Any(f => f.Name.Equals("TestMessage")));
        }

        [Test()]
        public void Subscribe_CheckUnsubscribeOfSubscriber_SusbcribersListShouldBeEmpty()
        {
            var sut = new MessageBrokerExt();
            var subscriber = new Mock<IMessageSubscriber>();
          
            var unSubscriber = sut.Subscribe<TestMessage>(subscriber.Object);
            unSubscriber.Dispose();

            Assert.IsTrue(sut.GetSubscribers().Count == 0);
        }



        private class TestMessage : Message
        {

        }

        private class MessageBrokerExt : MessageBroker
        {
            public Dictionary<Type, IPublishStrategy> GetStrategies()
            {
                return base.GetStrategies();
            }

            public Dictionary<Type, List<IMessageSubscriber>> GetSubscribers()
            {
                return base.GetSubscribers();
            }
        }

        private class PublishStrategy : IPublishStrategy
        {
            public void PublishAll(Message message, Dictionary<Type, List<IMessageSubscriber>> subscribers)
            {
                throw new NotImplementedException();
            }
        }
    }
}