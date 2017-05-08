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
            using (var context = new MyContext())
            {
                return context.Products.FirstOrDefault(p => p.Id == id);
            }
        }

        public List<Product> GetAll()
        {
            using (var context = new MyContext())
            {
                List<Product> products = context.Products.ToList();
                return products;
            }
        }

        public bool Delete(Guid id)
        {
            using (var context = new MyContext()) {
                Product product = context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return false;
                }
                context.Products.Remove(product);
                context.SaveChanges();
                return true;
            }
        }

        public bool Update(Guid id, Product p1)
        {
            using (var context = new MyContext())
            {
                if (this.Delete(id)) {
                    context.Products.Add(p1);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void EmptyDatabase() {
            using (var context = new MyContext())
            {
                context.Empty();
            }
        }
    }
}