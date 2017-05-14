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

        [TestMethod]
        public void LoginWithUsernameOkTest()
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

            string userUsername = "Mati";
            string hashedPass = EncryptionHelper.GetMD5("prueba1234");
            string token = service.Login(userUsername, hashedPass);
            Assert.AreEqual(token.Length, 32);
        }

        [TestMethod]
        public void LoginWithEmailOkTest()
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

            string userEmail = "matigru@gmail.com";
            string hashedPass = EncryptionHelper.GetMD5("prueba1234");
            string token = service.Login(userEmail, hashedPass);
            Assert.AreEqual(token.Length, 32);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void LoginNotExistingUsernameTest()
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

            string userUsername = "Matias4";
            string hashedPass = EncryptionHelper.GetMD5("prueba1234");
            string token = service.Login(userUsername, hashedPass);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void LoginNotExistingEmailTest()
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

            string userEmail = "matias@gmail.com";
            string hashedPass = EncryptionHelper.GetMD5("prueba1234");
            string token = service.Login(userEmail, hashedPass);
        }

        [ExpectedException(typeof(NoLoginDataMatchException))]
        [TestMethod]
        public void LoginNoDataMatchTest()
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

            string userEmail = "matigru@gmail.com";
            string hashedPass = EncryptionHelper.GetMD5("prueba442");
            string token = service.Login(userEmail, hashedPass);
        }

        [TestMethod]
        public void LogoutOkTest()
        {
            /*User u = new User();
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

            string userUsername = "Mati";
            string token = service.Login(userUsername, hashedPass);
            Assert.AreEqual(token.Length, 32);*/
            throw new NotImplementedException();
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void LogoutNoUserTest()
        {
            /*User u = new User();
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

            string userUsername = "Mati";
            string token = service.Login(userUsername, hashedPass);
            Assert.AreEqual(token.Length, 32);*/
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ChangeUserRoleOkTest()
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

            int newRole = 2;
            service.ChangeUserRole(u.Id, newRole);
            User aux = service.Get(u.Id);
            Assert.AreEqual(aux.Role, newRole);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void ChangeUserRoleNoUserTest()
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

            int newRole = 2;
            Guid wrongGuid = Guid.NewGuid();
            service.ChangeUserRole(wrongGuid, newRole);
        }

        [ExpectedException(typeof(NotExistingUserRoleException))]
        [TestMethod]
        public void ChangeUserRoleNoUserRoleTest()
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

            int newRole = 5;
            service.ChangeUserRole(u.Id, newRole);
        }

        [TestMethod]
        public void DeleteUserOkTest()
        {
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
            UserService service = getService();
            service.Register(u, a);

            User u2 = new User();
            u2.FirstName = "Martin";
            u2.LastName = "Musetti";
            u2.PhoneNumber = "+59899211266";
            u2.Password = "prueba12345";
            u2.Email = "martinmusetti@gmail.com";
            u2.Username = "Martin";
            Address a2 = new Address();
            a2.Street = "Miami";
            a2.StreetNumber = "1766";
            a2.PhoneNumber = "26002540";
            service.Register(u2, a2);

            service.Delete(u2.Id);
            Assert.AreEqual(service.GetAll().Count, 1);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void DeleteNoUserTest()
        {
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
            UserService service = getService();
            service.Register(u, a);

            User u2 = new User();
            u2.FirstName = "Martin";
            u2.LastName = "Musetti";
            u2.PhoneNumber = "+59899211266";
            u2.Password = "prueba12345";
            u2.Email = "martinmusetti@gmail.com";
            u2.Username = "Martin";
            Address a2 = new Address();
            a2.Street = "Miami";
            a2.StreetNumber = "1766";
            a2.PhoneNumber = "26002540";
            service.Register(u2, a2);

            Guid wrongId = Guid.NewGuid();
            service.Delete(wrongId);
        }

        [TestMethod]
        public void ChangePasswordOkTest()
        {
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
            UserService service = getService();
            service.Register(u, a);

            string oldPassword = u.Password;
            string newPassword = "pruebaNueva123";
            service.ChangePassword(u.Id, oldPassword, newPassword);
        }

        [ExpectedException(typeof(WrongPasswordException))]
        [TestMethod]
        public void ChangePasswordWrongOldPasswordTest()
        {
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
            UserService service = getService();
            service.Register(u, a);

            string oldPassword = "prueba433";
            string newPassword = "pruebaNueva123";
            service.ChangePassword(u.Id, oldPassword, newPassword);
        }

        [ExpectedException(typeof(WrongPasswordException))]
        [TestMethod]
        public void ChangePasswordWrongNewPasswordTest()
        {
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
            UserService service = getService();
            service.Register(u, a);

            string oldPassword = u.Password;
            string newPassword = "p";
            service.ChangePassword(u.Id, oldPassword, newPassword);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void ChangePasswordNoUserTest()
        {
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
            UserService service = getService();
            service.Register(u, a);

            string oldPassword = u.Password;
            string newPassword = "pruebA123";
            Guid wrongId = Guid.NewGuid();
            service.ChangePassword(wrongId, oldPassword, newPassword);
        }
        //LOGIN
        //Ok con username ----------
        //Ok con mail ----------
        //No existing username ----------
        //No existing email ----------
        //No match ----------

        //Logout
        //Ok
        //No user

        //CHANGE USER ROLE
        //Ok ----------
        //No User 1 (el que cambia) ----------NO VA!
        //No user 2 (el cambiado) ----------
        //Wrong User Role (usuario que cambia debe ser superadmin) ---------- NO VA!
        //Wrong User Role (rol inexistente) ---------- 

        //Modify
        //IDEM CREATE

        //DELETE
        //Ok usuario a si mismo ---------- HICE DOS-
        //Ok admin a usuario -----------
        //No user to delete ----------
        //No user deleting ---------NO VA!
        //User deleting no es superadmin y quiere borrar a otro user ---------NO VA!

        //CHANGE PASSWORD
        //Ok
        //Wrong Old Password
        //Wrong New Password
        //No User



    }
}
