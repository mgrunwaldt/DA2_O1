using System;

namespace Exceptions
{

    public class AddressWithoutPhoneNumberException : Exception
    {
        public AddressWithoutPhoneNumberException()
        {
        }

        public AddressWithoutPhoneNumberException(string message) : base(message)
        {
        }
    }
}