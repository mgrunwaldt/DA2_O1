namespace Exceptions
{
 
    public class AddressDeleteUserDoesntHaveException : System.Exception
    {
        public AddressDeleteUserDoesntHaveException()
        {
        }

        public AddressDeleteUserDoesntHaveException(string message) : base(message)
        {
        }
    }
}