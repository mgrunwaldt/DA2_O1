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
        public DbSet<Feature> Features { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MyContext>(null);
            Database.SetInitializer<MyContext>(new DropCreateDatabaseIfModelChanges<MyContext>());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<User>().HasMany(u => u.Addresses).WithMany(a => a.Users).Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("AddressId");
                m.ToTable("UserAddresses");
            });
            base.OnModelCreating(modelBuilder);
        }

        public void Empty() {
            foreach (ProductFeature pf in ProductFeatures) {
                this.ProductFeatures.Remove(pf);
            }
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
            foreach (Address a in Addresses) {
                this.Addresses.Remove(a);
            }
            foreach (Feature f in Features)
            {
                this.Features.Remove(f);
            }
            foreach (Order o in Orders)
            {
                this.Orders.Remove(o);
            }
            this.SaveChanges();
        }
    }
}
