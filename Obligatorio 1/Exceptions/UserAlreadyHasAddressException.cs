namespace Exceptions
{

    public class UserAlreadyHasAddressException : System.Exception
    {
        public UserAlreadyHasAddressException()
        {
        }

        public UserAlreadyHasAddressException(string message) : base(message)
        {
        }
    }
}