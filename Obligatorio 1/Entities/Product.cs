using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Product
    {
        public string Code { get; set; }
        public string Description { get; set; }

        [Key]
        public Guid Id { get; set; }

        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public virtual Category Category { get; set; }

        public Product() {
            this.Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() == typeof(Product)){
                Product p = (Product)obj;
                return p.Id == this.Id && p.Code == this.Code && p.Description == this.Description && p.Manufacturer == this.Manufacturer && p.Name == this.Name && p.Price == this.Price;
            }
            return false;
        }
    }
}
