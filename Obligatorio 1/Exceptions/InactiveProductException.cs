using System;

namespace Exceptions
{
    public class InactiveProductException : Exception
    {
        public InactiveProductException()
        {
        }

        public InactiveProductException(string message) : base(message)
        {
        }
    }
}