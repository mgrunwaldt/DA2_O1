﻿using System;
using Entities;
using Repository;
using Services.Interfaces;
using DataAccess;
using System.Linq;
using Exceptions;
using System.Collections.Generic;
using Services.Implementations;

namespace Services
{
    public class ProductService:IProductService
    {
        private GenericRepository<Product> repo;
        private GenericRepository<ProductFeature> productFeatureRepo;
        private GenericRepository<Feature> featureRepo;
        private GenericRepository<OrderProduct> orderProductRepo;
        private GenericRepository<Review> reviewRepo;
        public ProductService(GenericRepository<Product> repoInstance, GenericRepository<ProductFeature> productFeatureRepoInstance, GenericRepository<Feature> featureRepoInstance, GenericRepository<OrderProduct> orderProductRepoInstance,GenericRepository<Review> reviewRepoInstance)
        {
            this.repo = repoInstance;
            this.productFeatureRepo = productFeatureRepoInstance;
            this.featureRepo = featureRepoInstance;
            this.orderProductRepo = orderProductRepoInstance;
            this.reviewRepo = reviewRepoInstance;
        }


        public void Add(Product p)
        {
            p.Validate();
            checkForExistingProduct(p);
            setCategory(p);
            repo.Add(p);
        }

        public void Delete(Guid pId)
        {
            Product p = Get(pId);
            p.IsActive = false;
            repo.Update(p);
        }

        public void Modify(Product p)
        {
            checkIfProductExists(p.Id);
            p.Validate();
            checkForExistingProduct(p);
            setCategory(p);
            repo.Update(p);
        }

        public Product Get(Guid id, bool getReviews = false)
        {
            List<Product> allProducts = repo.GetAll();
            Product existing = allProducts.Find(prod => prod.Id == id && prod.IsActive == true);
            if (existing == null)
            {
                throw new ProductNotExistingException("No hay producto activo con este id");
            }
            existing.ProductFeatures = GetAllProductFeaturesFromProduct(existing);
            if (!getReviews) {
                Product copyWithoutReviews = new Product();
                copyWithoutReviews.Category = existing.Category;
                copyWithoutReviews.Code = existing.Code;
                copyWithoutReviews.Description = existing.Description;
                copyWithoutReviews.Id = existing.Id;
                copyWithoutReviews.Manufacturer = existing.Manufacturer;
                copyWithoutReviews.Name = existing.Name;
                copyWithoutReviews.Price = existing.Price;
                copyWithoutReviews.ProductFeatures = existing.ProductFeatures;
                copyWithoutReviews.ProductReviews = null;
                return copyWithoutReviews;
            }
            return existing;
        }

        public void ChangeCategory(Guid id, Guid cId)
        {
            Product p = Get(id);
            MyContext context = repo.GetContext();
            if (cId != Guid.Empty)
            {
                Category c = context.Categories.Where(ca => ca.Id == cId).FirstOrDefault();
                if (c == null)
                {
                    throw new ProductChangeCategoryException("La categoría nueva no existe");
                }
                if (p.Category != null) {
                    if (cId.Equals(p.Category.Id))
                        throw new ProductChangeCategoryException("El producto ya tiene esta categoría");
                }
                
                p.Category = c;
            }
            else p.Category = null;
            repo.Update(p);
        }

        public void AddProductFeature(ProductFeature productFeature)
        {
            checkIfProductExists(productFeature.ProductId); 
            checkIfProductAlreadyHasFeature(productFeature.FeatureId,productFeature.ProductId);
            Feature feature = getFeature(productFeature.FeatureId);
            productFeature.Validate();
            productFeature.CheckIfValueCorrespondsToType(feature);
            this.productFeatureRepo.Add(productFeature);
        }

        public void ModifyProductFeatureValue(Guid id, string val)
        {
            ProductFeature productFeature = productFeatureRepo.Get(id);
            if (productFeature != null)
            {
                productFeature.Value = val;
                productFeature.Validate();
                Feature feature = getFeature(productFeature.FeatureId);
                productFeature.CheckIfValueCorrespondsToType(feature);
                productFeatureRepo.Update(productFeature);
            }
            else throw new NoProductFeatureException("No hay atributo de producto con este id");
        }

        public List<ProductFeature> GetAllProductFeaturesFromProduct(Product p)
        {
            List<ProductFeature> allProductFeatures = productFeatureRepo.GetAll();
            List<ProductFeature> productFeatures = allProductFeatures.FindAll(pf => pf.ProductId == p.Id);
            return productFeatures;
        }

        private void checkIfProductAlreadyHasFeature(Guid featureId,Guid productId)
        {
            List<ProductFeature> allProductFeatures = productFeatureRepo.GetAll();
            ProductFeature existing = allProductFeatures.Find(pf => pf.ProductId == productId && pf.FeatureId == featureId);
            if (existing != null) {
                throw new ProductFeatureDuplicateFeature("Este producto ya tiene un valor para este atributo");
            }
        }

        private Feature getFeature(Guid featureId)
        {
            List<Feature> allFeatures = featureRepo.GetAll();
            Feature existing = allFeatures.Find(feature => feature.Id == featureId);
            if (existing == null)
            {
                throw new NoFeatureException("No existe este atributo");
            }
            return existing;

        }

        
        private void checkForExistingProduct(Product p)
        {
            List<Product> allProducts = repo.GetAll();
            Product existing = allProducts.Find(prod => (prod.Name == p.Name || prod.Code == p.Code) && prod.Id != p.Id && prod.IsActive == true);
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

       

        private void checkIfProductExists(Guid pId)
        {
            List<Product> allProducts = repo.GetAll();
            Product existing = allProducts.Find(prod => prod.Id == pId && prod.IsActive == true);
            if (existing == null)
            {
                throw new ProductNotExistingException("No existe este producto");
            }
        }

        public List<Product> GetAllFromOrder(Order order)
        {
            List<Product> ret = new List<Product>();
            List<OrderProduct> allOrderProducts = orderProductRepo.GetAll();
            foreach(var orderProduct in allOrderProducts)
            {
                if(orderProduct.OrderId == order.Id)
                {
                    ret.Add(repo.Get(orderProduct.ProductId));
                }
            }
            return ret;
        }
        public void RemoveFeatureFromProduct(Guid pId,Guid fId)
        {
            checkIfProductExists(pId);
            Feature savedFeature = getFeature(fId);
            List<ProductFeature> allProductFeatures = productFeatureRepo.GetAll();
            ProductFeature existing = allProductFeatures.Find(pf => pf.ProductId == pId && pf.FeatureId == fId);
            if (existing == null)
            {
                throw new ProductWithoutFeatureException("Este producto no está asociado a este atributo");
            }
            productFeatureRepo.Delete(existing);
        }

        public List<Product> GetAll(bool getReviews)
        {
            List<Product> allProducts = repo.GetAll();
            List<Product> productsToShow = new List<Product>();
            foreach (Product p in allProducts) {
                if (p.IsActive) {
                    Product toAdd = new Product();
                    toAdd.Category = p.Category;
                    toAdd.Code = p.Code;
                    toAdd.Description = p.Description;
                    toAdd.Id = p.Id;
                    toAdd.Manufacturer = p.Manufacturer;
                    toAdd.Name = p.Name;
                    toAdd.Price = p.Price;
                    toAdd.ProductFeatures = null;
                    if (getReviews)
                    {
                        toAdd.ProductReviews = p.ProductReviews;
                    }
                    else {
                        toAdd.ProductReviews = null;
                    }
                    productsToShow.Add(toAdd);
                }
            }
            return productsToShow;            
        }

        public List<Tuple<Product, int>> GetMostSold(int maxNumberOfProducts)
        {
            throw new NotImplementedException();
        }
    }
}