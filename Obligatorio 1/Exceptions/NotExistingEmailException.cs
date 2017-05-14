namespace Exceptions
{
    public class NotExistingEmailException : System.Exception
    {
        public NotExistingEmailException()
        {
        }

        public NotExistingEmailException(string message) : base(message)
        {
        }
    }
}
