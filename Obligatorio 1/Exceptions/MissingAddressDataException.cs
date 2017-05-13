using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class MissingAddressDataException : Exception
    {
        public MissingAddressDataException()
        {
        }

        public MissingAddressDataException(string message) : base(message)
        {
        }

        public MissingAddressDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingAddressDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}