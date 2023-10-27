using Smartfy.Core.Entities;

namespace Smartfy.Mqtt.Entity
{
    public sealed class MqttMessage : Message
    {
        public string Topic { get; init; }
    }
}
