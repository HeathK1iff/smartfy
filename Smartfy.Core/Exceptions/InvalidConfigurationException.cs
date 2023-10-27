using Smartfy.Core.Exceptions;
using System.Runtime.Serialization;

namespace Smartfy.Weather.Exceptions
{
    [Serializable]
    public class InvalidConfigurationException : SmartfyCoreException
    {
        public InvalidConfigurationException() : base("Configuration is invalid")
        {
        }

        public InvalidConfigurationException(string? message) : base(message)
        {
        }

        public InvalidConfigurationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}