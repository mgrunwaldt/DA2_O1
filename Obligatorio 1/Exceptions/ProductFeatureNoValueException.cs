namespace Exceptions
{

    public class ProductFeatureNoValueException : System.Exception
    {
        public ProductFeatureNoValueException()
        {
        }

        public ProductFeatureNoValueException(string message) : base(message)
        {
        }
    }
}