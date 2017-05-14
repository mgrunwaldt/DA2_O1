using System;

namespace Exceptions
{
    public class NotExistingUserRoleException : Exception
    {
        public NotExistingUserRoleException()
        {
        }

        public NotExistingUserRoleException(string message) : base(message)
        {
        }

    }
}