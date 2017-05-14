namespace Exceptions
{

    public class ProductWrongPriceException : System.Exception
    {
        public ProductWrongPriceException()
        {
        }

        public ProductWrongPriceException(string message) : base(message)
        {
        }
    }
}