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
            checkForExistingEmail(u);
            checkForExistingUsername(u);
            u.Password = EncryptionHelper.GetMD5(u.Password);
            userRepository.Add(u);
        }

        private void checkForExistingEmail(User u) {
            List<User> allUsers = userRepository.GetAll();
            var existingUser = allUsers.Find(user => user.Email == u.Email);
            if (existingUser != null) {
                throw new ExistingEmailException("Ya existe un usuario con este email");
            }
        }

        private void checkForExistingUsername(User u) {
            List<User> allUsers = userRepository.GetAll();
            var existingUser = allUsers.Find(user => user.Username == u.Username);
            if (existingUser != null) {
                throw new ExistingUsernameException("Ya existe un usuario con este nombre de usuario");
            }
        }
       
    }
}