using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        void Add(Category c);
        void Delete(Guid id);
        List<Category> GetAll();
        Category Get(Guid id);
        void Modify(Category c);
    }
}
