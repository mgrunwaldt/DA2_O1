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
    }
}
