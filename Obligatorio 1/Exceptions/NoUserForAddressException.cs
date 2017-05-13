namespace Exceptions
{

    public class NoUserForAddressException : System.Exception
    {
        public NoUserForAddressException()
        {
        }

        public NoUserForAddressException(string message) : base(message)
        {
        }
    }
}