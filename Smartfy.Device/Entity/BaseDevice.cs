using Smartfy.Core.Entities;
using Smartfy.Core.Messages;
using Smartfy.Core.Services.Messages;

namespace Smartfy.Device.Entity
{
    public abstract class BaseDevice : IMessageSubscriber
    {        
        public BaseDevice(IMessageService messageService, Guid id, string vendor, string model, string location, string connectionString) 
        {
            Id = id;
            Vendor = vendor;
            Model = model;
            Location = location;
            ConnectionString = connectionString;
        }

        public Guid Id { get; private set; }
        public string Vendor { get; private set; }
        public string Model { get; private set; }
        public string ConnectionString { get; private set; }
        public string Location { get; set; } = string.Empty;

        public abstract void OnReceived(Message message);

        public static BaseDevice CreateEmptyDevice()
        {
            return new EmptyDevice(null, Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        private class EmptyDevice : BaseDevice
        {
            public EmptyDevice(IMessageService broker, Guid id, string vendor, string model, string location, string connectionString) : base(broker, id, vendor, model, location, connectionString)
            {
            }

            public override void OnReceived(Message message)
            {
                throw new NotImplementedException();
            }
        }
    }

    
}
