using Smartfy.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Smartfy.Device.Exception
{
    public class NotFoundDefenitionException : SmartfyCoreException
    {
        public NotFoundDefenitionException()
        {
        }

        public NotFoundDefenitionException(string? message) : base(message)
        {
        }

        public NotFoundDefenitionException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundDefenitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
