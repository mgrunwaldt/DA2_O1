using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using Repository;
using System.Collections.Generic;

namespace DataAccessTests
{
    [TestClass]
    public class ProductAccessTests
    {
        [TestMethod]
        public void AddTest()
        {
            Product p = new Product();
            ProductsRepository pr = new ProductsRepository();
            pr.Add(p);
            Assert.AreNotEqual(1, p.Id);
        }

        [TestMethod]
        public void GetTest()
        {
            Product p = new Product();
            ProductsRepository pr = new ProductsRepository();
            pr.Add(p);
            Assert.Equals(pr.Get(p.Id), p);
        }

        [TestMethod]
        public void GetAllTest()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            Product p3 = new Product();
            ProductsRepository pr = new ProductsRepository();
            pr.Add(p1);
            pr.Add(p2);
            pr.Add(p3);
            List<Product> products = pr.GetAll();
            Assert.Equals(products.Count, 4);
            Assert.Equals(products[0], p1);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            ProductsRepository pr = new ProductsRepository();
            pr.Add(p1);
            pr.Add(p2);
            pr.Delete(p1.Id);
            List<Product> products = pr.GetAll();
            Assert.Equals(products.Count, 1);
            Assert.Equals(products[0], p2);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Product p1 = new Product();
            p1.Name = "Producto 1";
            p1.Code = "123456";
            p1.Description = "Descripcion p1";
            p1.Manufacturer = "Fabricante p1";
            p1.Price = 100;
            ProductsRepository pr = new ProductsRepository();
            pr.Add(p1);
            p1.Name = "Producto 1 Actualizado";
            pr.Update(p1.Id, p1);
            Product product = pr.Get(p1.Id);
            Assert.Equals(product.Name, "Producto 1 Actualizado");
        }
    }
}
