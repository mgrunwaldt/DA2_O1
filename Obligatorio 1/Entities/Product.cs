using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
