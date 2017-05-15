namespace Exceptions
{

    public class ProductWithoutFeatureException : System.Exception
    {
        public ProductWithoutFeatureException()
        {
        }

        public ProductWithoutFeatureException(string message) : base(message)
        {
        }
    }
}