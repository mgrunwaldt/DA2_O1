using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class UserWithoutActiveOrderException : Exception
    {
        public UserWithoutActiveOrderException()
        {
        }

        public UserWithoutActiveOrderException(string message) : base(message)
        {
        }

        public UserWithoutActiveOrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserWithoutActiveOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}