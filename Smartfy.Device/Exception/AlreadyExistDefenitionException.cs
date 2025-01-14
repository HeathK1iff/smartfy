using Smartfy.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Smartfy.Device.Exception
{
    public class AlreadyExistDefenitionException : SmartfyCoreException
    {
        public AlreadyExistDefenitionException()
        {
        }

        public AlreadyExistDefenitionException(string? message) : base(message)
        {
        }

        public AlreadyExistDefenitionException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected AlreadyExistDefenitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
