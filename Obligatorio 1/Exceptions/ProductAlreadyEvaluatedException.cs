namespace Exceptions
{

    public class ProductAlreadyEvaluatedException : System.Exception
    {
        public ProductAlreadyEvaluatedException()
        {
        }

        public ProductAlreadyEvaluatedException(string message) : base(message)
        {
        }
    }
}