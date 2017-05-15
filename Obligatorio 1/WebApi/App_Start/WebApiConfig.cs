using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Repository;
using Entities;
using DataAccess;

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
            container.RegisterType<IGenericRepository<User>, GenericRepository<User>>(new HierarchicalLifetimeManager());
            container.RegisterInstance<GenericRepository<User>>(userRepo);
            container.RegisterInstance<GenericRepository<Address>>(addressRepo);
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
