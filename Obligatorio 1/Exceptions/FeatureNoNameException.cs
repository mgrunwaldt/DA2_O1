using System;

namespace Exceptions
{
  
    public class FeatureNoNameException : Exception
    {
        public FeatureNoNameException()
        {
        }

        public FeatureNoNameException(string message) : base(message)
        {
        }
    }
}