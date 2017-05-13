namespace Exceptions
{
    public class MissingCategoryDataException : System.Exception
    {
        public MissingCategoryDataException()
        {
        }

        public MissingCategoryDataException(string message) : base(message)
        {
        }
    }
}