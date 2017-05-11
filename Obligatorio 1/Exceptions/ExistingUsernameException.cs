namespace Exceptions
{
    public class ExistingUsernameException: System.Exception
    {
        public ExistingUsernameException()
        {
        }

        public ExistingUsernameException(string message) : base(message)
        {
        }
    }
}