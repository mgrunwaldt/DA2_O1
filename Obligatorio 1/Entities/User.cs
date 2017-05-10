using System;

namespace Entities
{
    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Username { get; set; }
    }
}