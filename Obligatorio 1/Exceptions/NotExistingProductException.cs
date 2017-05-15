namespace Exceptions
{
    public class NotExistingProductException : System.Exception
    {
        public NotExistingProductException()
        {
        }

        public NotExistingProductException(string message) : base(message)
        {
        }
    }
}
