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
                throw new NoUserForAddressException("No se puede crear la dirección ya que no existe el usuario");
            }
        }

        public void AddAddress(Address a, User u)
        {
            //check for existing address (no duplicates)
            a.Validate();
            checkForExistingUser(u);
            Address existingAddress = getEqualAddress(a);
            u.Addresses.Add(existingAddress);
            userRepo.Update(u);
            /*
             u.Validate();
            checkForExistingEmail(u.Email);
            checkForExistingUsername(u.Username);
            checkPasswordFormat(u.Password);
            u.Password = EncryptionHelper.GetMD5(u.Password);
            u.PhoneNumber = PhoneHelper.GetPhoneWithCorrectFormat(u.PhoneNumber);
            EmailHelper.CheckEmailFormat(u.Email);
            a.Validate();
            u.Address = a;
            userRepository.Add(u);
             */
        }
    }
}
