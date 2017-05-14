using System;

namespace Exceptions
{
    public class FeatureWrongTypeException : Exception
    {
        public FeatureWrongTypeException()
        {
        }

        public FeatureWrongTypeException(string message) : base(message)
        {
        }
    }
}