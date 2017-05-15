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
        private IGenericRepository<Address> addressRepository;


        public UserService(IGenericRepository<User> repo, IGenericRepository<Address> addressRepo) {
            userRepository = repo;
            addressRepository = addressRepo;
        }
        public void Register(User u, Address a)
        {
            u.Validate();
            checkForExistingEmail(u, u.Email);
            checkForExistingUsername(u, u.Username);
            checkPasswordFormat(u.Password);
            u.Password = EncryptionHelper.GetMD5(u.Password);
            u.PhoneNumber = PhoneHelper.GetPhoneWithCorrectFormat(u.PhoneNumber);
            EmailHelper.CheckEmailFormat(u.Email);
            a.Validate();
            Address userAddress = getEqualAddress(a);
            u.Address = userAddress;
            u.Role = 1;
            userRepository.Add(u);
        }

        private Address getEqualAddress(Address a)
        {
            List<Address> allAddresses = addressRepository.GetAll();
            Address existing = allAddresses.Find(address => a.Street == address.Street && a.PhoneNumber == address.PhoneNumber && a.StreetNumber == address.StreetNumber);
            if (existing != null)
                return existing;
            return a;
        }

        private void checkPasswordFormat(String password) {
            if (password.ToCharArray().Length < 6) {
                throw new WrongPasswordException("La contraseña no puede tener menos de 6 caracteres");
            }
        }

        private void checkForExistingEmail(User u, String email) {
            List<User> allUsers = userRepository.GetAll();
            var existingUser = allUsers.Find(user => user.Email == email && user.Id != u.Id);
            if (existingUser != null) {
                    throw new ExistingUserException("Ya existe un usuario con este email");
            }
        }

        private void checkForExistingUsername(User u, String username) {
            List<User> allUsers = userRepository.GetAll();
            var existingUser = allUsers.Find(user => user.Username == username && user.Id != u.Id);
            if (existingUser != null) {
                    throw new ExistingUserException("Ya existe un usuario con este nombre de usuario");
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
                            userRepository.Update(user);
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
                            userRepository.Update(user);
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

        public User GetFromToken(string token) {
            if (token != null) {
                List<User> allUsers = userRepository.GetAll();
                User u = allUsers.Find(user => user.Token == token);
                if (u != null) {
                    return u;
                }
                throw new NoUserWithTokenException("No hay usuario con este token, por favor inicie sesión");
            }
            throw new NoTokenException("Debes mandar el token de sesión en los headers como 'Token'");
            

        }

        public void Logout(Guid id)
        {
            User u = userRepository.Get(id);
            if (u != null)
            {
                u.Token = "";
                userRepository.Update(u);
            }else
            {
                throw new NotExistingUserException();
            }
        }

        public void ChangeUserRole(Guid id, int role){
            User u = userRepository.Get(id);
            if (u != null)
            {
                if (role == 2 || role == 3)
                {
                    u.Role = role;
                    userRepository.Update(u);
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

        public void Delete(Guid id) {
            User u = userRepository.Get(id);
            if (u != null) {
                userRepository.Delete(id);
            }else
            {
                throw new NotExistingUserException();
            }
        }

        public void ChangePassword(Guid id, string oldPassword, string newPassword) {
            User u = userRepository.Get(id);
            if (u != null)
            {
                string oldPasswordHash = EncryptionHelper.GetMD5(oldPassword);
                if (oldPasswordHash == u.Password)
                {
                    checkPasswordFormat(newPassword);
                    string newPasswordHash = EncryptionHelper.GetMD5(newPassword);
                    u.Password = newPasswordHash;
                    userRepository.Update(u);
                }else
                {
                    throw new WrongPasswordException();
                }
            }else
            {
                throw new NotExistingUserException();
            }
        }

        public void Modify(User user)
        {
            List<User> all = userRepository.GetAll();
            user.Validate();
            checkForExistingEmail(user, user.Email);
            checkForExistingUsername(user, user.Username);
            user.PhoneNumber = PhoneHelper.GetPhoneWithCorrectFormat(user.PhoneNumber);
            EmailHelper.CheckEmailFormat(user.Email);
            userRepository.Update(user);
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