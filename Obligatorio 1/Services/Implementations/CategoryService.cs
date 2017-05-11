using Entities;
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
            throw new NotImplementedException();
        }

    }
}
