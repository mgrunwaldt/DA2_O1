namespace Exceptions
{

    public class ProductDuplicateException : System.Exception
    {
        public ProductDuplicateException()
        {
        }

        public ProductDuplicateException(string message) : base(message)
        {
        }
    }
}