using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using Services;
using Exceptions;

namespace ServicesTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void RegisterOkTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.Street = "Carlos Butler";
            u.StreetNumber = "1921";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = new UserService();
            service.Register(u);
            Assert.AreNotEqual(Guid.Empty, u.Id);
        }
        [ExpectedException(typeof(MissingUserDataException))]
        [TestMethod]
        public void RegisterMissingDataTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.Street = "Carlos Butler";
            u.StreetNumber = "1921";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = new UserService();
            service.Register(u);
        }

        [ExpectedException(typeof(WrongNumberFormatException))]
        [TestMethod]
        public void WrongNumberFormatTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.Street = "Carlos Butler";
            u.StreetNumber = "1921";
            u.PhoneNumber = "435345";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = new UserService();
            service.Register(u);
        }

        [TestMethod]
        public void RegisterDifferentNumberFormatTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.Street = "Carlos Butler";
            u.StreetNumber = "1921";
            u.PhoneNumber = "094606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            UserService service = new UserService();
            service.Register(u);
            Assert.AreNotEqual(Guid.Empty, u.Id);
            Assert.AreEqual(u.PhoneNumber, "+59894606123");
        }

        [ExpectedException(typeof(WrongEmailFormatException))]
        [TestMethod]
        public void WrongEmailFormatTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.Street = "Carlos Butler";
            u.StreetNumber = "1921";
            u.PhoneNumber = "435345";
            u.Password = "prueba1234";
            u.Email = "matigru";
            u.Username = "Mati";
            UserService service = new UserService();
            service.Register(u);
        }

        [ExpectedException(typeof(ExistingUsernameException))]
        [TestMethod]
        public void ExistingUsernameTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.Street = "Carlos Butler";
            u.StreetNumber = "1921";
            u.PhoneNumber = "435345";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";

            User u2 = new User();
            u2.FirstName = "Juan";
            u2.LastName = "Grunwaldt";
            u2.Street = "Carlos Butler";
            u2.StreetNumber = "1921";
            u2.PhoneNumber = "435345";
            u2.Password = "prueba1234";
            u2.Email = "juangru@gmail.com";
            u2.Username = "Mati";

            UserService service = new UserService();
            service.Register(u);
            service.Register(u2);
        }

        [ExpectedException(typeof(ExistingEmailException))]
        [TestMethod]
        public void ExistingEmailTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.Street = "Carlos Butler";
            u.StreetNumber = "1921";
            u.PhoneNumber = "435345";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";

            User u2 = new User();
            u2.FirstName = "Juan";
            u2.LastName = "Grunwaldt";
            u2.Street = "Carlos Butler";
            u2.StreetNumber = "1921";
            u2.PhoneNumber = "435345";
            u2.Password = "prueba1234";
            u2.Email = "matigru@gmail.com";
            u2.Username = "Juan";

            UserService service = new UserService();
            service.Register(u);
            service.Register(u2);
        }

        [ExpectedException(typeof(WrongPasswordException))]
        [TestMethod]
        public void WrongPasswordTest()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.Street = "Carlos Butler";
            u.StreetNumber = "1921";
            u.PhoneNumber = "435345";
            u.Password = "a";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
           
            UserService service = new UserService();
            service.Register(u);
        }


    }
}
