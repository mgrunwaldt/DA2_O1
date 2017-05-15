using System;

namespace Exceptions
{
    public class IncorrectOrderStatusException : Exception
    {
        public IncorrectOrderStatusException()
        {
        }

        public IncorrectOrderStatusException(string message) : base(message)
        {
        }
    }
}