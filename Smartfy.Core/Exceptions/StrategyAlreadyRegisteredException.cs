using System.Runtime.Serialization;

namespace Smartfy.Core.Exceptions
{
    public class StrategyAlreadyRegisteredException : SmartfyCoreException
    {
        public StrategyAlreadyRegisteredException(): this("Strategy already added for message type")
        {
        }

        public StrategyAlreadyRegisteredException(string? message) : base(message)
        {
        }

        public StrategyAlreadyRegisteredException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected StrategyAlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}