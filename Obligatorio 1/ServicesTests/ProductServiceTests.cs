using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace ServicesTests
{
    [TestClass]
    public class ProductServiceTests
    {
        private ProductService getService() {
            GenericRepository<Product> repoInstance = new GenericRepository<Product>(true);
            return new ProductService(repoInstance);
        }

        private CategoryService getCategoryService()
        {
            GenericRepository<Category> repoInstance = new GenericRepository<Category>();
            return new CategoryService(repoInstance);
        }

        private Category getCategory()
        {
            Category c = new Category();
            c.Name = "Nombre Cat";
            c.Description = "Description Cat";
            CategoryService service = getCategoryService();
            service.Add(c);
            return c;
        }

        
        [TestMethod]
        public void ProductCreateOkTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "Name";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);
            GenericRepository<Product> repo = new GenericRepository<Product>();
            Product savedProduct = repo.Get(p.Id);
            Assert.IsNotNull(savedProduct);
        }

        [TestMethod]
        public void ProductCreateNoCategoryOkTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "Name";
            p.Price = 100;
            service.Add(p);
            GenericRepository<Product> repo = new GenericRepository<Product>();
            Product savedProduct = repo.Get(p.Id);
            Assert.IsNotNull(savedProduct);
        }

        [ExpectedException(typeof(NotExistingCategoryException))]
        [TestMethod]
        public void ProductCreateWrongCategoryTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "Name";
            p.Price = 100;
            p.Category = new Category();
            service.Add(p);
        }

        [ExpectedException(typeof(ProductWithoutNameException))]
        [TestMethod]
        public void ProductCreateNoNameTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);
        }

        [ExpectedException(typeof(ProductWithoutCodeException))]
        [TestMethod]
        public void ProductCreateNoCodeTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "nombre";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);
        }

        [ExpectedException(typeof(ProductWithoutDescriptionException))]
        [TestMethod]
        public void ProductCreateNoDescriptionTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Manufacturer = "Manu";
            p.Name = "nombre";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);
        }

        [ExpectedException(typeof(ProductWithoutManufacturerException))]
        [TestMethod]
        public void ProductCreateNoManufacturerTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Name = "nombre";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);
        }

        [ExpectedException(typeof(ProductWithoutPriceException))]
        [TestMethod]
        public void ProductCreateNoPriceTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "nombre";
            p.Category = getCategory();
            service.Add(p);
        }

        [ExpectedException(typeof(ProductWrongPriceException))]
        [TestMethod]
        public void ProductCreateWrongPriceTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "nombre";
            p.Price = -100;
            p.Category = getCategory();
            service.Add(p);
        }

        [ExpectedException(typeof(ProductDuplicateException))]
        [TestMethod]
        public void ProductCreateDuplicateNamePriceTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "nombre";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);

            Product p = new Product();
            p.Code = "3333";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "nombre";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);
        }

        [ExpectedException(typeof(ProductDuplicateException))]
        [TestMethod]
        public void ProductCreateDuplicateCodePriceTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "3333";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "nombre";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);

            Product p = new Product();
            p.Code = "3333";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "Hola";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);
        }


        //MODIFY
        //OK
        //No category(bien)
        // Wrong Category
        //Product no User
        //No Name
        //No Code
        //No Description
        //No manufacturer
        //No Price
        //Negative Price
        //Existing Name
        //Existing Code
        //Wrong User Role
        //Not Existing Product
        //Existing Name
        //Existing Code

        //DELETE
        //OK
        //NO Product
        //Wrong User Role
        //Delete con ordenes con este producto (borro order products de ordenes haciendose), cambio active a 0

        //Change Category
        //Ok
        //Wrong Category
        //No category
        //No Product
        //Wrong User Role

        //ADD ATTRIBUTE
        //Ok
        //Already Added
        //NO Attribute
        //NO Produt
        //Wrong User Role
        //Value different from type


        //GET ALL 
        //Ok (no user)
        //Ok (user, with reviews)
        //Wrong User (Idem no user)

        //GET Filtered

        //GET - tambien devuelve attributtes
        //OK
        //Check attributes
        //No Product

        //GET MOST SOLD
        //ADD PICTURE
        //DELETE PICTURE





    }
}
