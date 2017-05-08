using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;

namespace DataAccess
{
    public class MyContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public void Empty() {
            foreach(Product p in Products)
            {
                this.Products.Remove(p);
            }
            this.SaveChanges();
        }
    }
}
