using Microsoft.Extensions.Logging;
using Smartfy.Core.Services.Messages;
using Smartfy.Device.Entity;
using System.Text.Json;

namespace Smartfy.Device.Xiaomi.Devices
{
    public class MCCGQ11LM : XiaomiBase, IContact
    {
        public MCCGQ11LM(IMessageService messageService, Guid id, string vendor, string model, string location, string connectionString) : base(messageService, id, vendor, model, location, connectionString)
        {
        }

        public TrackedValue<bool> Contact
        {
            get;
            private set;
        } = new TrackedValue<bool>(5);

        protected override void ParceValue(JsonElement element)
        {
            if (element.TryGetProperty("contact", out var propContact))
            {
                Contact.SetValue(propContact.GetBoolean());
                _logger.LogTrace($"Contact [{Id.ToString()}] = {Contact.GetValue()}");
            }
        }
    }
}