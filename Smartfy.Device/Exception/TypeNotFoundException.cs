using Smartfy.Core.Exceptions;
using System.Runtime.Serialization;

namespace Smartfy.Device.Exception
{
    public class TypeNotFoundException : SmartfyCoreException
    {
        public TypeNotFoundException()
        {
        }

        public TypeNotFoundException(string? message) : base(message)
        {
        }

        public TypeNotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected TypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
