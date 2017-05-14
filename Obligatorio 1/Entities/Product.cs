using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Exceptions;

namespace Entities
{
    public class Product
    {

        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public virtual Category Category { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid();
            this.IsActive = true;
        }


        public void Validate()
        {
            validateCode();
            validateDescription();
            validateManufacturer();
            validateName();
            validatePrice();
        }

        private void validatePrice()
        {
            if (Price == 0)
                throw new ProductMissingDataException("No se puede crear un producto sin precio");
            if (Price < 0)
                throw new ProductWrongPriceException("El precio de un producto no puede ser negativo");
        }

        private void validateManufacturer()
        {
            if (Manufacturer == null || Manufacturer.Trim() == "")
                throw new ProductMissingDataException("No se puede crear un producto sin proveedor");
        }

        private void validateName()
        {
            if (Name == null || Name.Trim() == "")
                throw new ProductMissingDataException("No se puede crear un producto sin nombre");
        }

        private void validateDescription()
        {
            if (Description == null || Description.Trim() == "")
                throw new ProductMissingDataException("No se puede crear un producto sin descripción");
        }

        private void validateCode()
        {
            if (Code == null || Code.Trim() == "")
                throw new ProductMissingDataException("No se puede crear un producto sin código");
        }



       

        public override bool Equals(object obj)
        {
            if(obj.GetType() == typeof(Product)){
                Product p = (Product)obj;
                return p.Code == this.Code || p.Name == this.Name;
            }
            return false;
        }

    }
}
