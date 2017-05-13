using Entities;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Services;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesTests
{
    /*
      
    public class UserServiceTests
    {
        private UserService getService() {
            GenericRepository<User> repo = new GenericRepository<User>(true);
            return new UserService(repo);
        }

        [TestMethod]
         */
    [TestClass]
    public class AddresServiceTests
    {
        private AddressService getService()
        {
            GenericRepository<Address> repo = new GenericRepository<Address>(true);
            return new AddressService(repo);
        }
        private UserService getUserService()
        {
            GenericRepository<User> repo = new GenericRepository<User>();
            return new UserService(repo);
        }

        private User getUser() {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            UserService service = getUserService();
            service.Register(u, a);
            return u;
        }

        private User getOtherUser()
        {
            User u = new User();
            u.FirstName = "Juan";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "juan@gmail.com";
            u.Username = "Juan";
            Address a = new Address();
            a.Street = "Jamaica";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            UserService service = getUserService();
            service.Register(u, a);
            return u;
        }

        [TestMethod]
        public void CreateAddressOkTest() {
            Address a = new Address();
            a.Street = "Cartagena";
            a.StreetNumber = "1582";
            a.PhoneNumber = "26003564";
            AddressService service = getService();
            User u = getUser();
            service.AddAddress(a, u);       
            Assert.AreNotEqual(Guid.Empty, a.Id);
        }

        [ExpectedException(typeof(NoUserForAddressException))]
        [TestMethod]
        public void CreateAddressNoUserTest()
        {
            Address a = new Address();
            a.Street = "Cartagena";
            a.StreetNumber = "1582";
            a.PhoneNumber = "26003564";
            AddressService service = getService();
            User u = new User();
            service.AddAddress(a, u);
        }

        [ExpectedException(typeof(AddressWithoutStreetException))]
        [TestMethod]
        public void CreateAddressNoStreetTest()
        {
            Address a = new Address();
            a.StreetNumber = "1582";
            a.PhoneNumber = "26003564";
            AddressService service = getService();
            User u = getUser();
            service.AddAddress(a, u);
        }

        [ExpectedException(typeof(AddressWithoutStreetNumberException))]
        [TestMethod]
        public void CreateAddressNoStreetNumberTest()
        {
            Address a = new Address();
            a.Street = "Cartagena";
            a.PhoneNumber = "26003564";
            AddressService service = getService();
            User u = getUser();
            service.AddAddress(a, u);
        }

        [ExpectedException(typeof(AddressWithoutPhoneNumberException))]
        [TestMethod]
        public void CreateAddressNoPhoneNumberTest()
        {
            Address a = new Address();
            a.Street = "Cartagena";
            a.StreetNumber = "1582";
            AddressService service = getService();
            User u = getUser();
            service.AddAddress(a, u);
        }

        [TestMethod]
        public void AddExistingAddressToDifferentUserTest()
        {
            Address a = new Address();
            a.Street = "Cartagena";
            a.StreetNumber = "1582";
            a.PhoneNumber = "26003564";

            Address a2 = new Address();
            a2.Street = "Cartagena";
            a2.StreetNumber = "1582";
            a2.PhoneNumber = "26003564";
            AddressService service = getService();
            User u = getUser();
            User u2 = getOtherUser();
            service.AddAddress(a, u);
            service.AddAddress(a2, u2);
            List<Address> userOneAddresses = u.Addresses;
            List<Address> userTwoAddresses = u2.Addresses;
            bool userOneHasAddress = userOneAddresses.Exists(address => address.Id == a.Id);
            bool userTwoHasAddress = userTwoAddresses.Exists(address => address.Id == a2.Id);

            Assert.IsTrue(userOneHasAddress);
            Assert.IsTrue(userTwoHasAddress);
            Assert.AreEqual(a.Id, a2.Id);
        }

        [TestMethod]
        public void AddMultipleAddressesToOneUser()
        {
            Address a = new Address();
            a.Street = "Cartagena";
            a.StreetNumber = "1582";
            a.PhoneNumber = "26003564";

            Address a2 = new Address();
            a2.Street = "Miami";
            a2.StreetNumber = "2222";
            a2.PhoneNumber = "26203564";

            AddressService service = getService();
            User u = getUser();
            service.AddAddress(a, u);
            service.AddAddress(a2, u);
            List<Address> userOneAddresses = u.Addresses;
            bool userHasAddressOne = userOneAddresses.Exists(address => address.Id == a.Id);
            bool userHasAddressTwo = userOneAddresses.Exists(address => address.Id == a2.Id);

            Assert.IsTrue(userHasAddressOne);
            Assert.IsTrue(userHasAddressOne);
        }

        

        //Add Twice to User
        //Delete Ok
        //Delete Error
        //Get All From User
        //Get Default From User


    }
}
