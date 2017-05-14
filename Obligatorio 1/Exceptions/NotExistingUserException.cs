namespace Exceptions
{
    public class NotExistingUserException : System.Exception
    {
        public NotExistingUserException()
        {
        }

        public NotExistingUserException(string message) : base(message)
        {
        }
    }
}
