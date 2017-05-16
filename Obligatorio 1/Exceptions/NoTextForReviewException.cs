using System;

namespace Exceptions
{
    public class NoTextForReviewException : Exception
    {
        public NoTextForReviewException()
        {
        }

        public NoTextForReviewException(string message) : base(message)
        {
        }

    }
}