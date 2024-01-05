using Microsoft.Extensions.Logging;
using Smartfy.Core.Entities;
using Smartfy.Core.Services.Messages;
using Smartfy.Device.Entity;
using Smartfy.Mqtt.Entity;
using System.Text.Json;

namespace Smartfy.Device.Xiaomi.Devices
{
    public abstract class XiaomiBase : BaseDevice, IBattery, IBatteryVoltage, ILinkQuality
    {
        protected readonly ILogger _logger;
        protected XiaomiBase(IMessageService messageService, Guid id, string vendor, string model, string location, string connectionString) : base(messageService, id, vendor, model, location, connectionString)
        {
            messageService.Subscribe<MqttMessage>(this);
            _logger = Device.LoggerFactory.CreateLogger<XiaomiBase>();
            _logger.LogDebug($"Created device {id.ToString()}");
        }

        public TrackedValue<int> Battery
        {
            get;
            private set;
        } = new TrackedValue<int>(5);

        public TrackedValue<int> BatteryVoltage
        {
            get;
            private set;
        } = new TrackedValue<int>(5);

        public TrackedValue<int> LinkQuality
        {
            get;
            private set;
        } = new TrackedValue<int>(5);

        public override void OnReceived(Message message)
        {
            MqttMessage incomeMessage = message as MqttMessage;

            if (!incomeMessage.Topic.Equals(ConnectionString, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var parcedObject = JsonDocument.Parse(incomeMessage.Data);

            if (parcedObject.RootElement.TryGetProperty("battery", out var propBattery))
            {
                Battery.SetValue(propBattery.GetInt32());
                _logger.LogTrace($"Battery [{Id.ToString()}] = {Battery.GetValue()}");
            }

            if (parcedObject.RootElement.TryGetProperty("voltage", out var propVoltage))
            {
                BatteryVoltage.SetValue(propBattery.GetInt32());
                _logger.LogTrace($"Voltage [{Id.ToString()}] = {BatteryVoltage.GetValue()}");
            }

            if (parcedObject.RootElement.TryGetProperty("linkquality", out var prop))
            {
                LinkQuality.SetValue(prop.GetInt32());
                _logger.LogTrace($"LinkQuality [{Id.ToString()}] = {LinkQuality.GetValue()}");
            }

            ParceValue(parcedObject.RootElement);

            _logger.LogDebug($"Updated values for {Id.ToString()} device");
        }

        protected abstract void ParceValue(JsonElement element);
    }
}