using System.Runtime.Serialization;

namespace Smartfy.Core.Exceptions
{
    public class TaskException : SmartfyCoreException
    {
        public TaskException()
        {
        }

        public TaskException(string? message) : base(message)
        {
        }

        public TaskException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected TaskException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
