using Entities;
using Exceptions;
using Repository;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private IGenericRepository<Category> categoryRepository;

        public CategoryService(IGenericRepository<Category> repo)
        {
            categoryRepository = repo;
        }

        public void Add(Category c)
        {
            c.Validate();
            checkForExistingName(c.Name);
            categoryRepository.Add(c);
        }

        private void checkForExistingName(string name)
        {
            List<Category> allCategories = categoryRepository.GetAll();
            var existingCategory = allCategories.Find(category => category.Name == name);
            if (existingCategory != null)
            {
                throw new ExistingCategoryNameException("Ya existe una categoria con este nombre");
            }
        }

        public void Delete(System.Guid id) {
            bool deleted = categoryRepository.Delete(id);
            if (!deleted) {
                throw new NotExistingCategoryException();
            }
        }

        public List<Category> GetAll()
        {
            return categoryRepository.GetAll();
        }

        public Category Get(Guid id) {
            return categoryRepository.Get(id);
        }

        public void Modify(Category c, string name, string description)
        {
            checkForExistingName(name);
            c.Name = name;
            c.Description = description;
            c.Validate();
            categoryRepository.Update(c);
        }
    }
}
