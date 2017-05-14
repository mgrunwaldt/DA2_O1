using System;
using Entities;
using Repository;
using Services.Interfaces;
using DataAccess;
using System.Linq;
using Exceptions;
using System.Collections.Generic;

namespace Services
{
    public class ProductService:IProductService
    {
        private GenericRepository<Product> repo;


        public ProductService(GenericRepository<Product> repoInstance)
        {
            this.repo = repoInstance;
        }

        public void Add(Product p)
        {
            p.Validate();
            checkForExistingProduct(p);
            setCategory(p);
            repo.Add(p);
        }

        private void checkForExistingProduct(Product p)
        {
            List<Product> allProducts = repo.GetAll();
            Product existing = allProducts.Find(prod => prod == p);
            if (existing != null) {
                throw new ProductDuplicateException("Ya existe un producto con este nombre y/o código");
            }
        }

        private void setCategory(Product p)
        {
            if (p.Category != null)
            {
                MyContext context = repo.GetContext();
                Category c = context.Categories.Where(ca => ca.Id == p.Category.Id).FirstOrDefault();
                if (c == null)
                    throw new NotExistingCategoryException("La categoría asignada a este producto no existe");
                p.Category = c;
            }
        }
    }
}