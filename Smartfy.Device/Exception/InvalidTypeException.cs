using Smartfy.Core.Exceptions;
using System.Runtime.Serialization;

namespace Smartfy.Device.Exception
{
    public class InvalidTypeException : SmartfyCoreException
    {
        public InvalidTypeException()
        {
        }

        public InvalidTypeException(string? message) : base(message)
        {
        }

        public InvalidTypeException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
