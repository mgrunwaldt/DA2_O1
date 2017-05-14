namespace Exceptions
{
    public class ProductChangeCategoryException : System.Exception
    {
        public ProductChangeCategoryException()
        {
        }

        public ProductChangeCategoryException(string message) : base(message)
        {
        }
    }
}