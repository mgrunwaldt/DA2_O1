using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace Repository
{
    public class ProductsRepository
    {
        public ProductsRepository(bool forTest = false) {
            if (forTest) {
                EmptyDatabase();
            }
        }
        public void Add(Product p)
        {
            using (var context = new MyContext()) {
                p.Id = Guid.NewGuid();
                context.Products.Add(p);
                context.SaveChanges();
            }
        }
        //HOLA
        public Product Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Product p1)
        {
            throw new NotImplementedException();
        }

        public void EmptyDatabase() {
            using (var context = new MyContext())
            {
                context.Empty();
            }
        }
    }
}