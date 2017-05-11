using Entities;
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

    }
}
