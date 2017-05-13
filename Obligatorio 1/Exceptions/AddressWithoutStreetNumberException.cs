using System;

namespace Exceptions
{
    public class AddressWithoutStreetNumberException : Exception
    {
        public AddressWithoutStreetNumberException()
        {
        }

        public AddressWithoutStreetNumberException(string message) : base(message)
        {
        }
    }
}