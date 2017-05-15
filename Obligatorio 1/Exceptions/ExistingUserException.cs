namespace Exceptions
{
    public class ExistingUserException: System.Exception
    {
        public ExistingUserException()
        {
        }

        public ExistingUserException(string message) : base(message)
        {
        }
    }
}