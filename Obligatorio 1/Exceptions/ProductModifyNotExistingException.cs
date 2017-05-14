namespace Exceptions
{

    public class ProductModifyNotExistingException : System.Exception
    {
        public ProductModifyNotExistingException()
        {
        }

        public ProductModifyNotExistingException(string message) : base(message)
        {
        }
    }
}