using System;

namespace Exceptions
{
    
    public class FeatureExistingCombinationException : Exception
    {
        public FeatureExistingCombinationException()
        {
        }

        public FeatureExistingCombinationException(string message) : base(message)
        {
        }
    }
}