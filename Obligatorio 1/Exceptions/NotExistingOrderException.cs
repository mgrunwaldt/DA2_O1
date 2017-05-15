namespace Exceptions
{
    public class NotExistingOrderException : System.Exception
    {
        public NotExistingOrderException()
        {
        }

        public NotExistingOrderException(string message) : base(message)
        {
        }
    }
}