using System.Runtime.Serialization;

namespace Smartfy.TelegramBot.Exceptions
{
    public class TokenNotDefinedException : System.Exception
    {
        public TokenNotDefinedException()
        {
        }

        public TokenNotDefinedException(string? message) : base(message)
        {
        }

        public TokenNotDefinedException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected TokenNotDefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}