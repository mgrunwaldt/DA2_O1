using System;

namespace Exceptions
{
    public class ExistingEmailException: Exception
    {
        public ExistingEmailException()
        {
        }

        public ExistingEmailException(string message) : base(message)
        {
        }
    }
}