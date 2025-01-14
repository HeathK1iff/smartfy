using System.Runtime.Serialization;

namespace Smartfy.Core.Exceptions
{
    public class SmartfyCoreException : System.Exception
    {
        public SmartfyCoreException()
        {
        }

        public SmartfyCoreException(string? message) : base(message)
        {
        }

        public SmartfyCoreException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SmartfyCoreException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
