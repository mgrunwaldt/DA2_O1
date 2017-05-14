using System;

namespace Exceptions
{
      public class FeatureWithoutTypeException : Exception
    {
        public FeatureWithoutTypeException()
        {
        }

        public FeatureWithoutTypeException(string message) : base(message)
        {
        }
    }
}