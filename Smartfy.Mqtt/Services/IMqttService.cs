using Smartfy.Core.Services;

namespace Smartfy.Mqtt.Services
{
    public interface IMqttService: IService
    {
        void Start();
        void Stop();
    }
}
