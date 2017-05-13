namespace Exceptions
{
    public class NotExistingCategoryException : System.Exception
    {
        public NotExistingCategoryException()
        {
        }

        public NotExistingCategoryException(string message) : base(message)
        {
        }
    }
}