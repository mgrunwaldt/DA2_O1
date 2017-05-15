using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class NoTokenException : Exception
    {
        public NoTokenException()
        {
        }

        public NoTokenException(string message) : base(message)
        {
        }

        public NoTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}