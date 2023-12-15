using System.Runtime.Serialization;

namespace Smartfy.Core.Exceptions
{
    [Serializable]
    public class GroupNotFoundException : SmartfyCoreException
    {
        public GroupNotFoundException()
        {
        }

        public GroupNotFoundException(string? groupName) : base($"Group {groupName} is not found")
        {
        }

        public GroupNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}