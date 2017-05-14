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
using Services;

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

        [ExpectedException(typeof(ProductMissingDataException))]
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

        [ExpectedException(typeof(ProductMissingDataException))]
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

        [ExpectedException(typeof(ProductMissingDataException))]
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

        [ExpectedException(typeof(ProductMissingDataException))]
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

        [ExpectedException(typeof(ProductMissingDataException))]
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
        public void ProductCreateDuplicateNameTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "nombre";
            p.Price = 100;
            Category cat = getCategory();
            p.Category = cat;
            service.Add(p);

            Product p2 = new Product();
            p2.Code = "3333";
            p2.Description = "Desc";
            p2.Manufacturer = "Manu";
            p2.Name = "nombre";
            p2.Price = 100;
            p2.Category = cat;
            service.Add(p2);
        }

        [ExpectedException(typeof(ProductDuplicateException))]
        [TestMethod]
        public void ProductCreateDuplicateCodeTest()
        {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "3333";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "nombre";
            p.Price = 100;
            Category cat = getCategory();

            p.Category = cat;
            service.Add(p);

            Product p2 = new Product();
            p2.Code = "3333";
            p2.Description = "Desc";
            p2.Manufacturer = "Manu";
            p2.Name = "Hola";
            p2.Price = 100;
            p2.Category = cat;
            service.Add(p2);
        }

        [TestMethod]
        public void ProductModifyOkTest() {
            ProductService service = getService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "Name";
            p.Price = 100;
            p.Category = getCategory();
            service.Add(p);
            p.Name = "Cambiado";
            service.Modify(p);
            GenericRepository<Product> repo = new GenericRepository<Product>();
            Product savedProduct = repo.Get(p.Id);
            Assert.IsNotNull(savedProduct);
            Assert.AreEqual("Cambiado",savedProduct.Name);
        }

        [TestMethod]
        public void ProductModifyNoCategoryOkTest()
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
            p.Category = null;
            service.Modify(p);
            GenericRepository<Product> repo = new GenericRepository<Product>();
            Product savedProduct = repo.Get(p.Id);
            Assert.IsNotNull(savedProduct);
            Assert.IsNull(savedProduct.Category);
        }

        [ExpectedException(typeof(NotExistingCategoryException))]
        [TestMethod]
        public void ProductModifyWrongCategoryTest()
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
            p.Category = new Category();
            service.Modify(p);
        }

        [ExpectedException(typeof(ProductMissingDataException))]
        [TestMethod]
        public void ProductModifyNoNameTest()
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
            p.Name = "";
            service.Modify(p);
        }

        [ExpectedException(typeof(ProductMissingDataException))]
        [TestMethod]
        public void ProductModifyNoCodeTest()
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
            p.Code = "";
            service.Modify(p);
        }

        [ExpectedException(typeof(ProductMissingDataException))]
        [TestMethod]
        public void ProductModifyNoDescriptionTest()
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
            p.Description = "";
            service.Modify(p);
        }

        [ExpectedException(typeof(ProductMissingDataException))]
        [TestMethod]
        public void ProductModifyNoManufacturerTest()
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
            p.Manufacturer = "";
            service.Modify(p);
        }

        [ExpectedException(typeof(ProductMissingDataException))]
        [TestMethod]
        public void ProductModifyNoPriceTest()
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
            p.Price = 0;
            service.Modify(p);
        }

        [ExpectedException(typeof(ProductWrongPriceException))]
        [TestMethod]
        public void ProductModifyNegativePriceTest()
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
            p.Price = -10;
            service.Modify(p);
        }

        [ExpectedException(typeof(ProductDuplicateException))]
        [TestMethod]
        public void ProductModifyDuplicateNameTest()
        {
            ProductService service = getService();

            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "Name";
            p.Price = 100;
            Category cat = getCategory();

            p.Category = cat;
            service.Add(p);

            Product p2 = new Product();
            p2.Code = "12345";
            p2.Description = "Desc";
            p2.Manufacturer = "Manu";
            p2.Name = "Nombre";
            p2.Price = 100;
            p2.Category = cat;
            service.Add(p2);

            p2.Name = "Name";
            service.Modify(p2);
        }

        [ExpectedException(typeof(ProductDuplicateException))]
        [TestMethod]
        public void ProductModifyDuplicateCodeTest()
        {
            ProductService service = getService();

            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "Name";
            p.Price = 100;
            Category cat = getCategory();
            p.Category = cat;
            service.Add(p);

            Product p2 = new Product();
            p2.Code = "12345";
            p2.Description = "Desc";
            p2.Manufacturer = "Manu";
            p2.Name = "Nombre";
            p2.Price = 100;
            p2.Category = cat;
            service.Add(p2);

            p2.Code = "1234";
            service.Modify(p2);
        }

        [ExpectedException(typeof(ProductModifyNotExistingException))]
        [TestMethod]
        public void ProductModifyNotExistingTest() {
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "Name";
            p.Price = 100;
            ProductService service = getService();
            service.Modify(p);
        }
      

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
