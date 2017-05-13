using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repository;

namespace Services.Implementations
{
    public class AddressService : IAddressService
    {
        private IGenericRepository<Address> addressRepo;

        public AddressService(IGenericRepository<Address> repo)
        {
            addressRepo = repo;
        }

        public void AddAddress(Address a, User u)
        {
            throw new NotImplementedException();
        }
    }
}
