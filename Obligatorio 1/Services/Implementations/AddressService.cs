using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repository;
using Exceptions;

namespace Services.Implementations
{
    public class AddressService : IAddressService
    {
        private IGenericRepository<Address> addressRepo;
        private IGenericRepository<User> userRepo;

        public AddressService(IGenericRepository<Address> addressRepoParam, IGenericRepository<User> userRepoParam)
        {
            addressRepo = addressRepoParam;
            userRepo = userRepoParam;
        }

        private Address getEqualAddress(Address a) {
            List<Address> allAddresses = addressRepo.GetAll();
            Address existing = allAddresses.Find(address => a.Street == address.Street && a.PhoneNumber == address.PhoneNumber && a.StreetNumber == address.StreetNumber);
            if (existing != null)
                return existing;
            return a;      
        }

        private void checkForExistingUser(User u) {
            User existing = userRepo.Get(u.Id);
            if (existing == null) {
                throw new NoUserForAddressException("No existe usuario");
            }
        }

        private void checkForExistingAddress(Address a)
        {
            Address existing = addressRepo.Get(a.Id);
            if (existing == null)
            {
                throw new AddressDeleteNoAddressException("No se puede borrar ya que no existe tal dirección");
            }
        }

        public void AddAddress(Address a, User u)
        {
            a.Validate();
            checkForExistingUser(u);
            Address existingAddress = getEqualAddress(a);
            checkIfUserHasAddress(u, a);
            u.Addresses.Add(existingAddress);
            userRepo.Update(u);
         
        }

        private void checkIfUserHasAddress(User u, Address a)
        {
            if (u.Address == a) {
                throw new UserAlreadyHasAddressException("El usuario ya tiene esta dirección");
            }
            List<Address> userAddresses = u.Addresses.ToList();
            if(userAddresses.Exists(address=>address.Street == a.Street && address.PhoneNumber == a.PhoneNumber && address.StreetNumber == a.StreetNumber))
                throw new UserAlreadyHasAddressException("El usuario ya tiene esta dirección");
        }

        public void RemoveAddress(Address a2, User u)
        {
            checkForExistingUser(u);
            checkForExistingAddress(a2);
            if (u.Address == a2)
            {
                removeDefaultAddress(a2, u);
            }
            else removeListAddress(a2, u);
            userRepo.Update(u);
        }

        private void removeDefaultAddress(Address a2, User u)
        {
            if (u.Addresses.Count() != 0)
            {
                Address firstAddress = u.Addresses.First();
                u.Address = firstAddress;
                u.Addresses.Remove(firstAddress);
            }
            else throw new AddressDeleteDefaultNoReplacementException("Para borrar la dirección con la que se registró debe tener por lo menos una dirección asociada");
        }

        private void removeListAddress(Address a2, User u)
        {
            if (u.Addresses.Contains(a2))
                u.Addresses.Remove(a2);
            else throw new AddressDeleteUserDoesntHaveException("Este usuario no tiene esta dirección");
        }

        public List<Address> GetAllAddresses(User u)
        {
            checkForExistingUser(u);

            List<Address> allAddresses = new List<Address>();
            allAddresses.AddRange(u.Addresses);
            allAddresses.Add(u.Address);
            return allAddresses;
        }
    }
}
