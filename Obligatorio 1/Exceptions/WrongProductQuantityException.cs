using System;

namespace Exceptions
{
    public class WrongProductQuantityException : Exception
    {
        public WrongProductQuantityException()
        {
        }

        public WrongProductQuantityException(string message) : base(message)
        {
        }
    }
}