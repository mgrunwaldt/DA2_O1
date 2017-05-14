namespace Exceptions
{

    public class ProductFeatureWrongValueException : System.Exception
    {
        public ProductFeatureWrongValueException()
        {
        }

        public ProductFeatureWrongValueException(string message) : base(message)
        {
        }
    }
}