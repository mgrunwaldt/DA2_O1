using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccess
{
    public class MyContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MyContext>(null);
            Database.SetInitializer<MyContext>(new DropCreateDatabaseIfModelChanges<MyContext>());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public void Empty() {
            foreach(Product p in Products)
            {
                this.Products.Remove(p);
            }
            foreach (User u in Users)
            {
                this.Users.Remove(u);
            }
            foreach (Category c in Categories)
            {
                this.Categories.Remove(c);
            }
            this.SaveChanges();
        }
    }
}
