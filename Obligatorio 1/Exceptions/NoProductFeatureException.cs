using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    [Serializable]
    public class NoProductFeatureException : Exception
    {
        public NoProductFeatureException()
        {
        }

        public NoProductFeatureException(string message) : base(message)
        {
        }

        public NoProductFeatureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoProductFeatureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}