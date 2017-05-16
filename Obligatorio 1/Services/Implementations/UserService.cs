using System;
using Entities;
using Repository;
using Exceptions;
using System.Collections.Generic;
using Tools;
using Entities.Statuses_And_Roles;
using Services.Interfaces;

namespace Services
{
    public class UserService : IUserService
    {

        private IGenericRepository<User> userRepository;
        private IGenericRepository<Order> orderRepository;
        private IGenericRepository<Address> addressRepository;
        private IGenericRepository<OrderProduct> orderProductRepository;
        private IGenericRepository<Product> productRepository;


        public UserService(IGenericRepository<User> repo, IGenericRepository<Order> orderRepo, IGenericRepository<Address> addressRepo, IGenericRepository<OrderProduct> orderProductRepo, IGenericRepository<Product> productRepo) {
            userRepository = repo;
            orderRepository = orderRepo;
            addressRepository = addressRepo;
            orderProductRepository = orderProductRepo;
            productRepository = productRepo;
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
            Order order = new Order();
            order.Status = OrderStatuses.WAITING_FOR_ADDRESS;
            order.UserId = u.Id;
            userRepository.Add(u);
            orderRepository.Add(order);
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
                            List<Order> allOrders = orderRepository.GetAll();
                            Order userActiveOrder = allOrders.Find(o => o.UserId == user.Id && o.Status == OrderStatuses.WAITING_FOR_ADDRESS);
                            List<OrderProduct> allOrderProducts = orderProductRepository.GetAll();
                            List<OrderProduct> productsFromActiveOrder = allOrderProducts.FindAll(op => op.OrderId == userActiveOrder.Id);
                            List<Product> productsToBuy = new List<Product>();
                            string productsToBuyString = " En el carrito tienes los siguientes productos: ";

                            foreach (OrderProduct orderProductToBuy in productsFromActiveOrder)
                            {
                                Product p = productRepository.Get(orderProductToBuy.ProductId);
                                if (p != null && p.IsActive)
                                {
                                    if (!productsToBuy.Contains(p))
                                    {
                                        productsToBuy.Add(p);
                                        productsToBuyString += (p.Name + " ");
                                    }
                                }
                            }

                            List<Order> ordersToEvaluate = allOrders.FindAll(o => o.UserId == user.Id && o.Status == OrderStatuses.PAYED);
                            List<Product> productsToEvaluate = new List<Product>();
                            string productsToEvaluateString = " Debes evaluar los siguientes productos: ";
                            foreach (Order orderToEvaluate in ordersToEvaluate) {
                                List<OrderProduct> productsFromOrder = allOrderProducts.FindAll(op => op.OrderId == userActiveOrder.Id);
                                foreach (OrderProduct orderProductToEvaluate in productsFromOrder) {
                                    Product p = productRepository.Get(orderProductToEvaluate.ProductId);
                                    if (p != null && p.IsActive) {
                                        if (!productsToEvaluate.Contains(p)) {
                                            productsToEvaluate.Add(p);
                                            productsToEvaluateString += (p.Name +" ");
                                        }
                                    }
                                }
                            }
                            if (productsToBuyString != " En el carrito tienes los siguientes productos: ")
                                token += productsToBuyString;
                            if (productsToEvaluateString != " Debes evaluar los siguientes productos: ")
                                token += productsToEvaluateString;
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
                if (role == 1 || role == 2 || role == 3)
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
            User u = this.Get(user.Id);
            
            if(u != null){
                string pass = u.Password;
                Address add = u.Address;
                List <User> all = userRepository.GetAll();
                user.Validate();
                checkForExistingEmail(user, user.Email);
                checkForExistingUsername(user, user.Username);
                user.PhoneNumber = PhoneHelper.GetPhoneWithCorrectFormat(user.PhoneNumber);
                user.Password = pass;
                user.Address = add;
                EmailHelper.CheckEmailFormat(user.Email);
                userRepository.Update(user);
            }else
            {
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