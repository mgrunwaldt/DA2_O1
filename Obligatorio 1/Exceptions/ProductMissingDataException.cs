namespace Exceptions
{

    public class ProductMissingDataException : System.Exception
    {
        public ProductMissingDataException()
        {
        }

        public ProductMissingDataException(string message) : base(message)
        {
        }
    }
}