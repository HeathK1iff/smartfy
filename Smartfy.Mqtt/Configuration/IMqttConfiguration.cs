namespace Smartfy.Mqtt.Configuration
{
    public interface IMqttConfiguration
    {
        string ClientId { get; set; }
        string Host { get; set; }
        string SubsribeTopic { get; set; }
    }
}