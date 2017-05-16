﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Repository;
using Entities;
using DataAccess;
using Services.Interfaces;
using Services;
using Services.Implementations;

namespace WebApi
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            MyContext context = new MyContext();
            GenericRepository<User> userRepo = new GenericRepository<User>(context);
            GenericRepository<Address> addressRepo = new GenericRepository<Address>(context);
            GenericRepository<Order> orderRepo = new GenericRepository<Order>(context);
            GenericRepository<Category> categoryRepo = new GenericRepository<Category>(context);
            GenericRepository<Product> productRepo = new GenericRepository<Product>(context);
            GenericRepository<Feature> featureRepo = new GenericRepository<Feature>(context);
            GenericRepository<ProductFeature> productFeatureRepo = new GenericRepository<ProductFeature>(context);
            GenericRepository<OrderProduct> orderProductRepo = new GenericRepository<OrderProduct>(context);
            GenericRepository<Review> reviewRepo = new GenericRepository<Review>(context);


            UserService userService = new UserService(userRepo,orderRepo, addressRepo);
            AddressService addressService = new AddressService(addressRepo,userRepo);
            CategoryService categoryService = new CategoryService(categoryRepo,productRepo);

            ProductService productService = new ProductService(productRepo, productFeatureRepo, featureRepo, orderProductRepo,reviewRepo,orderRepo);
            FeatureService featureService = new FeatureService(featureRepo);
            OrderService orderService = new OrderService(orderRepo, orderProductRepo,userRepo,productRepo);
            ReviewService reviewService = new ReviewService(reviewRepo, productRepo, userRepo, orderRepo, orderProductRepo);
            container.RegisterInstance<IUserService>(userService);
            container.RegisterInstance<IAddressService>(addressService);
            container.RegisterInstance<ICategoryService>(categoryService);
            container.RegisterInstance<IProductService>(productService);
            container.RegisterInstance<IFeatureService>(featureService);
            container.RegisterInstance<IOrderService>(orderService);
            container.RegisterInstance<IReviewService>(reviewService);
            config.DependencyResolver = new UnityResolver(container);


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
