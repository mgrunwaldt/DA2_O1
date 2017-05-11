using System;
using Entities;
using Repository;
using Exceptions;
using System.Collections.Generic;
using Tools;
namespace Services
{
    public class UserService:IUserService
    {

        private IGenericRepository<User> userRepository;

        public UserService(IGenericRepository<User> repo) {
            userRepository = repo;
        }
        public void Register(User u)
        {
            u.Validate();
            checkForExistingEmail(u.Email);
            checkForExistingUsername(u.Username);
            checkPasswordFormat(u.Password);
            u.Password = EncryptionHelper.GetMD5(u.Password);
            u.PhoneNumber = PhoneHelper.GetPhoneWithCorrectFormat(u.PhoneNumber);
            EmailHelper.CheckEmailFormat(u.Email);
            userRepository.Add(u);
        }

        private void checkPasswordFormat(String password) {
            if (password.ToCharArray().Length < 6) {
                throw new WrongPasswordException("La contraseña no puede tener menos de 6 caracteres");
            }
        }

        private void checkForExistingEmail(String email) {
            List<User> allUsers = userRepository.GetAll();
            var existingUser = allUsers.Find(user => user.Email == email);
            if (existingUser != null) {
                throw new ExistingEmailException("Ya existe un usuario con este email");
            }
        }

        private void checkForExistingUsername(String username) {
            List<User> allUsers = userRepository.GetAll();
            var existingUser = allUsers.Find(user => user.Username == username);
            if (existingUser != null) {
                throw new ExistingUsernameException("Ya existe un usuario con este nombre de usuario");
            }
        }
       
    }
}