using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class UserDoesntHaveAddressException : Exception
    {
        public UserDoesntHaveAddressException()
        {
        }

        public UserDoesntHaveAddressException(string message) : base(message)
        {
        }

        public UserDoesntHaveAddressException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserDoesntHaveAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}