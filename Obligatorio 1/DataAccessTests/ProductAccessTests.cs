using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using Repository;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace DataAccessTests
{
    [TestClass]
    public class ProductAccessTests
    {
        private MyContext context;
        private MyContext getContext()
        {
            if (context == null)
                context = new MyContext();
            return context;
        }
        [TestMethod]
        public void AddTest()
        {
            Product p = new Product();
            p.Name = "puto";
            GenericRepository<Product> pr = new GenericRepository<Product>(getContext(),true);
            pr.Add(p);
            Assert.AreNotEqual(Guid.Empty, p.Id);
        }

        [TestMethod]
        public void GetTest()
        {
            Product p = new Product();
            GenericRepository<Product> pr = new GenericRepository<Product>(getContext(),true);
            pr.Add(p);
            Product p2 = pr.Get(p.Id);
            Assert.AreEqual(p2, p);
        }

        [TestMethod]
        public void GetAllTest()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            Product p3 = new Product();
            GenericRepository<Product> pr = new GenericRepository<Product>(getContext(),true);
            pr.Add(p1);
            pr.Add(p2);
            pr.Add(p3);
            List<Product> products = pr.GetAll();
            Assert.AreEqual(products.Count, 3);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Product p1 = new Product();
            Product p2 = new Product();
            GenericRepository<Product> pr = new GenericRepository<Product>(getContext(),true);
            pr.Add(p1);
            pr.Add(p2);
            pr.Delete(p1.Id);
            List<Product> products = pr.GetAll().ToList();
            Assert.AreEqual(products.Count, 1);
            Assert.AreEqual(products[0], p2);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Product p1 = new Product();
            p1.Name = "Producto 555";
            p1.Code = "123456";
            p1.Description = "Descripcion p1";
            p1.Manufacturer = "Fabricante p1";
            p1.Price = 100;
            GenericRepository<Product> pr = new GenericRepository<Product>(getContext(),true);
            pr.Add(p1);
            p1.Name = "Producto 1 Actualizado";
            pr.Update(p1);
            Product product = pr.Get(p1.Id);
            Assert.AreEqual(product.Name, "Producto 1 Actualizado");
        }
    }
}
