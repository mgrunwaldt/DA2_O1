using System;
using Entities;
using Repository;

namespace Services
{
    public class ProductService
    {
        private GenericRepository<Product> repoInstance;

        public ProductService(GenericRepository<Product> repoInstance)
        {
            this.repoInstance = repoInstance;
        }

        public void Add(Product p)
        {
            throw new NotImplementedException();
        }
    }
}