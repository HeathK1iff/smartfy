using Smartfy.Core.Exceptions;
using System.Runtime.Serialization;

namespace Smartfy.Calendar.Exception
{
    public class ArgumentParceException : SmartfyCoreException
    {
        public ArgumentParceException()
        {
        }

        public ArgumentParceException(string? message) : base(message)
        {
        }

        public ArgumentParceException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected ArgumentParceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}