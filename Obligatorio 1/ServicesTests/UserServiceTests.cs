using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using Services;
using Exceptions;
using Repository;
using Tools;
namespace ServicesTests
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService getService() {
            GenericRepository<User> repo = new GenericRepository<User>(true);
            return new UserService(repo);
        }

        [TestMethod]
        public void RegisterOkTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u,a);
            Assert.AreNotEqual(Guid.Empty, u.Id);
        }

        [ExpectedException(typeof(MissingUserDataException))]
        [TestMethod]
        public void RegisterMissingFirstNameTest()
        {
            User u = new User();
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [ExpectedException(typeof(MissingUserDataException))]
        [TestMethod]
        public void RegisterMissingLastNameTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();

            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [ExpectedException(typeof(AddressWithoutStreetException))]
        [TestMethod]
        public void RegisterMissingStreetTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [ExpectedException(typeof(AddressWithoutStreetNumberException))]
        [TestMethod]
        public void RegisterMissingStreetNumberTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }
        //falta missing address phone
        [ExpectedException(typeof(MissingUserDataException))]
        [TestMethod]
        public void RegisterMissingPhoneNumberTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [ExpectedException(typeof(MissingUserDataException))]
        [TestMethod]
        public void RegisterMissingPasswordTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [ExpectedException(typeof(MissingUserDataException))]
        [TestMethod]
        public void RegisterMissingEmailTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [ExpectedException(typeof(MissingUserDataException))]
        [TestMethod]
        public void RegisterMissingUsernameTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [ExpectedException(typeof(WrongNumberFormatException))]
        [TestMethod]
        public void WrongNumberFormatTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "435345";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [TestMethod]
        public void RegisterDifferentNumberFormatTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "094606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
            Assert.AreNotEqual(Guid.Empty, u.Id);
            Assert.AreEqual(u.PhoneNumber, "0059894606123");
        }

        [ExpectedException(typeof(WrongEmailFormatException))]
        [TestMethod]
        public void WrongEmailFormatTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "094555666";
            u.Password = "prueba1234";
            u.Email = "matigru";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [ExpectedException(typeof(ExistingUsernameException))]
        [TestMethod]
        public void ExistingUsernameTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "094606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";

            User u2 = new User();
            u2.FirstName = "Juan";
            u2.LastName = "Grunwaldt";
            u2.PhoneNumber = "094660123";
            u2.Password = "prueba1234";
            u2.Email = "juangru@gmail.com";
            u2.Username = "Mati";

            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            Address a2 = new Address();
            a2.Street = "Carlos Butler";
            a2.StreetNumber = "1923";
            a2.PhoneNumber = "26007264";
            service.Register(u, a);
            service.Register(u2,a2);
        }

        [ExpectedException(typeof(ExistingEmailException))]
        [TestMethod]
        public void ExistingEmailTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "099888333";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";

            User u2 = new User();
            u2.FirstName = "Juan";
            u2.LastName = "Grunwaldt";
            u2.PhoneNumber = "0059894606123";
            u2.Password = "prueba1234";
            u2.Email = "matigru@gmail.com";
            u2.Username = "Juan";

            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            Address a2 = new Address();
            a2.Street = "Carlos Butler";
            a2.StreetNumber = "1923";
            a2.PhoneNumber = "26007264";
            service.Register(u, a);
            service.Register(u2, a2);
        }

        [ExpectedException(typeof(WrongPasswordException))]
        [TestMethod]
        public void WrongPasswordTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "435345";
            u.Password = "a";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
           
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
        }

        [TestMethod]
        public void Md5Test() {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
            Assert.AreNotEqual(Guid.Empty, u.Id);
            GenericRepository<User> ur = new GenericRepository<User>();
            User savedUser = ur.Get(u.Id);
            Assert.AreEqual(EncryptionHelper.GetMD5("prueba1234"), savedUser.Password);
        }

        [TestMethod]
        public void RegisterWithAddressTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);
            Assert.AreNotEqual(Guid.Empty, u.Id);
            Assert.AreNotEqual(Guid.Empty, u.Address.Id);
        }

        [TestMethod]
        public void TwoUsersOneAddressTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = getService();
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            service.Register(u, a);

            User u2 = new User();
            u2.FirstName = "Juan";
            u2.LastName = "Grunwaldt";
            u2.PhoneNumber = "+59894606123";
            u2.Password = "prueba1234";
            u2.Email = "juang@gmail.com";
            u2.Username = "Juan";
            Address a2 = new Address();
            a2.Street = "Carlos Butler";
            a2.StreetNumber = "1921";
            a2.PhoneNumber = "26007263";
            service.Register(u2, a2);


            Assert.AreEqual(u.Address.Id, u2.Address.Id);

        }

        //LOGIN
        //Ok
        //No existing username
        //No existing email
        //No match

        //Logout
        //Ok
        //No user

        //CHANGE USER ROLE
        //Ok
        //No User 1 (el que cambia)
        //No user 2 (el cambiado)
        //Wrong User Role (usuario que cambia debe ser superadmin)
        //Wrong User Role (rol inexistente)

        //Modify
        //IDEM CREATE

        //DELETE
        //Ok usuario a si mismo
        //Ok admin a usuario
        //No user to delete
        //No user deleting
        //User deleting no es superadmin y quiere borrar a otro user

        //CHANGE PASSWORD
        //Ok
        //Wrong Old Password
        //Wrong New Password
        //No User



    }
}
