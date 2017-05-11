using System;

namespace Exceptions
{
    public class ExistingCategoryNameException : Exception
    {
        public ExistingCategoryNameException()
        {
        }

        public ExistingCategoryNameException(string message) : base(message)
        {
        }

    }
}