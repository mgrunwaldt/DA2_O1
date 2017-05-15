namespace Exceptions
{
    public class NotExistingOrderWithCorrectStatusException : System.Exception
    {
        public NotExistingOrderWithCorrectStatusException()
        {
        }

        public NotExistingOrderWithCorrectStatusException(string message) : base(message)
        {
        }
    }
}