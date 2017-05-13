﻿using Entities;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesTests
{
    [TestClass]
    public class CategoryServiceTest
    {
        private CategoryService getService()
        {
            GenericRepository<Category> repo = new GenericRepository<Category>(true);
            return new CategoryService(repo);
        }

        [TestMethod]
        public void CreateCategoryOkTest()
        {
            Category c = new Category();
            c.Name = "Cat1";
            c.Description = "Desc.";
            CategoryService service = getService();
            service.Add(c);
            Assert.AreNotEqual(Guid.Empty, c.Id);
        }

        [ExpectedException(typeof(MissingCategoryDataException))]
        [TestMethod]
        public void MissingCategoryNameTest()
        {
            Category c = new Category();
            c.Description = "Desc";
            CategoryService service = getService();
            service.Add(c);
        }

        [ExpectedException(typeof(MissingCategoryDataException))]
        [TestMethod]
        public void MissingCategoryDescriptionTest()
        {
            Category c = new Category();
            c.Name = "Cat";
            CategoryService service = getService();
            service.Add(c);
        }

        [ExpectedException(typeof(ExistingCategoryNameException))]
        [TestMethod]
        public void ExistingCategoryNameTest()
        {
            Category c = new Category();
            c.Name = "Category";
            c.Description = "Desc.";

            Category c2 = new Category();
            c2.Name = "Category";
            c2.Description = "Description1";

            CategoryService service = getService();
            service.Add(c);
            service.Add(c2);
        }

        [TestMethod]
        public void DeleteCategoryOkTest()
        {
            Category c1 = new Category();
            c1.Name = "Category";
            c1.Description = "Desc.";
            Category c2 = new Category();
            c2.Name = "Category 2";
            c2.Description = "Desc. Cat. 2";

            CategoryService service = getService();

            service.Add(c1);
            service.Add(c2);
            service.Delete(c1.Id);

            List<Category> categories = service.GetAll();
            Assert.Equals(categories.Count, 1);
            Assert.Equals(categories[0], c2);
        }

        [ExpectedException(typeof(NotExistingCategoryException))]
        [TestMethod]
        public void DeleteNotExistingCategoryTest()
        {
            Category c1 = new Category();
            c1.Name = "Category";
            c1.Description = "Desc.";
            Category c2 = new Category();
            c2.Name = "Category 2";
            c2.Description = "Desc. Cat. 2";

            CategoryService service = getService();

            service.Add(c1);
            service.Add(c2);

            byte[] bytes = new byte[16];
            BitConverter.GetBytes(3).CopyTo(bytes, 0);
            Guid idToDelete = new Guid(bytes);
            service.Delete(idToDelete);
        }

    }
}
