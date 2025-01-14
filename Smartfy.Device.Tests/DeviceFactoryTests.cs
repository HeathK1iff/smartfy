using Moq;
using NUnit.Framework;
using Smartfy.Core.Entities;
using Smartfy.Core.Services.Messages;
using Smartfy.SmartHome.Entity;
using Smartfy.SmartHome.Utils;

namespace Smartfy.SmartHome.Tests
{

    public class MyDevice : SmartDevice
    {
        public MyDevice(IMessageService messageService, Guid id, string vendor, string model, string connectionString) : base(messageService, id, vendor, model, connectionString)
        {
        }

        public override void OnReceived(Message message)
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class DeviceFactoryTests
    { 
        [Test]
        public void CreateDevice_CreateDeviceInstanceByVendorModel_True()
        {
            var broker = new Mock<IMessageService>();
            var dev = new DeviceFactory(broker.Object);
            dev.Register("vendor_1", "model_1", typeof(MyDevice));

            var device = dev.CreateDevice(Guid.NewGuid(), "vendor_1", "model_1", "");



            
            Assert.IsTrue(device is MyDevice);
            Assert.AreEqual(device.Model, "model_1");
            Assert.AreEqual(device.Vendor, "vendor_1");
        }

    }
}
