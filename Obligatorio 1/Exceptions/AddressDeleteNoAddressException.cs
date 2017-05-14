namespace Exceptions
{

    public class AddressDeleteNoAddressException : System.Exception
    {
        public AddressDeleteNoAddressException()
        {
        }

        public AddressDeleteNoAddressException(string message) : base(message)
        {
        }
    }
}