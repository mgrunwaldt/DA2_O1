namespace Exceptions
{
 
    public class ProductDeleteNotExistingException : System.Exception
    {
        public ProductDeleteNotExistingException()
        {
        }

        public ProductDeleteNotExistingException(string message) : base(message)
        {
        }
    }
}