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
            checkForExistingName(c);
            categoryRepository.Add(c);
        }

        private void checkForExistingName(Category c)
        {
            List<Category> allCategories = categoryRepository.GetAll();
            var existingCategory = allCategories.Find(category => category.Name == c.Name);
            if (existingCategory != null)
            {
                throw new ExistingCategoryNameException("Ya existe un usuario con este email");
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

        public void Modify(Category c)
        {
            throw new NotImplementedException();
        }
    }
}
