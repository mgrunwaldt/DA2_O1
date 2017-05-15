using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class NoUserWithTokenException : Exception
    {
        public NoUserWithTokenException()
        {
        }

        public NoUserWithTokenException(string message) : base(message)
        {
        }

        public NoUserWithTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoUserWithTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}