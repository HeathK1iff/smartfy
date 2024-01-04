using Smartfy.Core.Services.Messages;
using Smartfy.Device.Entity;
using System.Text.Json;

namespace Smartfy.Device.Xiaomi.Devices
{
    public class WSDCGQ11LM : XiaomiBase, IHumidity, IPressure, ITemperature
    {
        public WSDCGQ11LM(IMessageService messageService, Guid id, string vendor, string model, string location, string connectionString) : base(messageService, id, vendor, model, location,connectionString)
        {
        }

        public TrackedValue<float> Humidity
        {
            get;
            private set;
        } = new TrackedValue<float>(10);

        public TrackedValue<float> Pressure
        {
            get;
            private set;
        } = new TrackedValue<float>(10);

        public TrackedValue<float> Temperature
        {
            get;
            private set;
        } = new TrackedValue<float>(10);

        protected override void ParceValue(JsonElement element)
        {
            if (element.TryGetProperty("humidity", out var propHumidity))
            {
                Humidity.SetValue(propHumidity.GetSingle());
            }

            if (element.TryGetProperty("temperature", out var propTemp))
            {
                Temperature.SetValue(propTemp.GetSingle());
            }

            if (element.TryGetProperty("pressure", out var propPressure))
            {
                Pressure.SetValue(propPressure.GetSingle());
            }
        }
    }
}