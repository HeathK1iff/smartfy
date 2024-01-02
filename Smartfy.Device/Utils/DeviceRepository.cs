using Smartfy.Device.Entity;
using Smartfy.Device.Exception;
using System.Text.Json;

namespace Smartfy.Device.Utils
{
    internal class DeviceRepository : IDeviceRepository, IDisposable
    {
        private FileStream _stream;
        public DeviceRepository(FileStream stream) 
        {
            _stream = stream;
        }

        public void Add(DeviceDef deviceDef)
        {
            var listOfDevices = new List<DeviceDef>(GetAll());
            
            if (string.IsNullOrEmpty(deviceDef.Id))
            {
                throw new ArgumentException(nameof(deviceDef.Id));
            }

            if (listOfDevices.Any(f => f.Id.Equals(deviceDef.Id)))
            {
                throw new AlreadyExistDefenitionException();
            }
            
            listOfDevices.Add(deviceDef);

            PostToStream(listOfDevices.ToArray());
        }

        public void Dispose()
        {
            _stream.Close();
            _stream.Dispose();
        }
       
        public DeviceDef[] GetAll()
        {
            _stream.Seek(0, SeekOrigin.Begin);
            
            if (_stream.Length == 0)
            {
                return new DeviceDef[0];
            }

            return JsonSerializer.Deserialize<DeviceDef[]>(_stream) ?? new DeviceDef[0];    
        }

        public void Remove(DeviceDef deviceDef)
        {
            var listOfDevices = new List<DeviceDef>(GetAll());

            int indexOfDevice = listOfDevices.IndexOf(deviceDef);

            if (indexOfDevice < 0) 
            {
                throw new NotFoundDefenitionException();
            }

            listOfDevices.RemoveAt(indexOfDevice);
            PostToStream(listOfDevices.ToArray());
        }

        private void PostToStream(DeviceDef[] items)
        {
            _stream.Seek(0, SeekOrigin.Begin);
            JsonSerializer.Serialize(_stream, items);
            _stream.Flush();
        }
    }
}
