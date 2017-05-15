namespace Exceptions
{
    public class NotExistingProductInOrderException : System.Exception
    {
        public NotExistingProductInOrderException()
        {
        }

        public NotExistingProductInOrderException(string message) : base(message)
        {
        }
    }
}
