namespace Exceptions
{

    public class ProductNotExistingException : System.Exception
    {
        public ProductNotExistingException()
        {
        }

        public ProductNotExistingException(string message) : base(message)
        {
        }
    }
}