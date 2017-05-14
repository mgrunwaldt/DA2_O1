namespace Exceptions
{
    public class NotExistingUsernameException : System.Exception
    {
        public NotExistingUsernameException()
        {
        }

        public NotExistingUsernameException(string message) : base(message)
        {
        }
    }
}
