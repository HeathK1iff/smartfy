using Smartfy.Core.Services.Messages;
using Smartfy.Device.Entity;
using System.Text.Json;

namespace Smartfy.Device.Xiaomi
{
    public class WSDCGQ11LM : XiaomiBase, IHumidity, IPressure, ITemperature 
    {
        public WSDCGQ11LM(IMessageService messageService, Guid id, string vendor, string model, string connectionString) : base(messageService, id, vendor, model, connectionString)
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
            Humidity.SetValue(element.GetProperty("humidity").GetSingle());
            Pressure.SetValue(element.GetProperty("pressure").GetSingle());
            Temperature.SetValue(element.GetProperty("temperature").GetSingle());
        }
    }
}