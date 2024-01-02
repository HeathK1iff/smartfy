using Smartfy.Core.Exceptions;
using System.Runtime.Serialization;

namespace Smartfy.Device.Exception
{
    public class AlreadyTypeExistException : SmartfyCoreException
    {
        public AlreadyTypeExistException()
        {
        }

        public AlreadyTypeExistException(string? message) : base(message)
        {
        }

        public AlreadyTypeExistException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected AlreadyTypeExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
