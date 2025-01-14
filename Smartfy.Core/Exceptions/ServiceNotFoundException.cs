using System.Runtime.Serialization;

namespace Smartfy.Core.Exceptions
{
    public class ServiceNotFoundException : SmartfyCoreException
    {
        public ServiceNotFoundException()
        {
        }

        public ServiceNotFoundException(string? message) : base(message)
        {
        }

        public ServiceNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ServiceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
