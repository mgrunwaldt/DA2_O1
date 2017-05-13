using System;

namespace Exceptions
{
    public class AddressWithoutStreetException : Exception
    {
        public AddressWithoutStreetException()
        {
        }

        public AddressWithoutStreetException(string message) : base(message)
        {
        }
    }
}