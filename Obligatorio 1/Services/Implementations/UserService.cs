using System;
using Entities;
using Repository;
using Exceptions;
using System.Collections.Generic;
using Tools;
namespace Services
{
    public class UserService : IUserService
    {

        private IGenericRepository<User> userRepository;

        public UserService(IGenericRepository<User> repo) {
            userRepository = repo;
        }
        public void Register(User u, Address a)
        {
            u.Validate();
            checkForExistingEmail(u.Email);
            checkForExistingUsername(u.Username);
            checkPasswordFormat(u.Password);
            u.Password = EncryptionHelper.GetMD5(u.Password);
            u.PhoneNumber = PhoneHelper.GetPhoneWithCorrectFormat(u.PhoneNumber);
            EmailHelper.CheckEmailFormat(u.Email);
            a.Validate();
            u.Address = a;
            u.Role = 1;
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

        public string Login(string identifier, string password) {
            List<User> users = userRepository.GetAll();
            if (identifier.Contains("@"))
            {
                foreach (var user in users) {
                    if (user.Email == identifier) {
                        if (user.Password == password)
                        {
                            string token = TokenHelper.CreateToken();
                            user.Token = token;
                            return token;
                        }
                        else {
                            throw new NoLoginDataMatchException("Contraseña incorrecta para ese usuario");
                        }
                    }
                }
                throw new NotExistingUserException("No existe el email especificado");
            }
            else {
                foreach (var user in users)
                {
                    if (user.Username == identifier)
                    {
                        if (user.Password == password)
                        {
                            string token = TokenHelper.CreateToken();
                            user.Token = token;
                            return token;
                        }
                        else
                        {
                            throw new NoLoginDataMatchException("Contraseña incorrecta para ese usuario");
                        }
                    }
                }
                throw new NotExistingUserException("No existe el nombre de usuario especificado");
            }
        }

        public void ChangeUserRole(Guid id, int role){
            User u = userRepository.Get(id);
            if (u != null)
            {
                if (role == 2 || role == 3)
                {
                    u.Role = role;
                }
                else
                {
                    throw new NotExistingUserRoleException();
                }
            }
            else {
                throw new NotExistingUserException();
            }
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User Get(Guid id)
        {
            return userRepository.Get(id);
        }


    }
}