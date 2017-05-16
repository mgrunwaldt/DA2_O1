﻿using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Statuses_And_Roles;
using Services.Implementations;
using Exceptions;
using DataAccess;

namespace ServicesTests
{
    [TestClass]
    public class OrderServiceTests
    {
        private MyContext context;
        private MyContext getContext()
        {
            if (context == null)
                context = new MyContext();
            return context;
        }
        private UserService getUserService()
        {
            GenericRepository<User> repo = new GenericRepository<User>(getContext());
            GenericRepository<Order> orderRepo = new GenericRepository<Order>(getContext());
            GenericRepository<Address> addressRepo = new GenericRepository<Address>(getContext());
            GenericRepository<OrderProduct> orderProductRepo = new GenericRepository<OrderProduct>(getContext());
            GenericRepository<Product> productRepo = new GenericRepository<Product>(getContext());
            return new UserService(repo, orderRepo, addressRepo, orderProductRepo, productRepo);
        }

        private OrderService getOrderService()
        {
            GenericRepository<Order> orderRepo = new GenericRepository<Order>(getContext(), true);
            GenericRepository<OrderProduct> orderProductRepo = new GenericRepository<OrderProduct>(getContext());
            GenericRepository<User> userRepo = new GenericRepository<User>(getContext());
            GenericRepository<Product> productRepo = new GenericRepository<Product>(getContext());
            return new OrderService(orderRepo, orderProductRepo, userRepo, productRepo);
        }

        private User registerUser()
        {
            User u = new User();
            u.FirstName = "Matias";
            u.LastName = "Grunwaldt";
            u.PhoneNumber = "+59894606123";
            u.Password = "prueba1234";
            u.Email = "matigru@gmail.com";
            u.Username = "Mati";
            Address a = new Address();
            a.Street = "Carlos Butler";
            a.StreetNumber = "1921";
            a.PhoneNumber = "26007263";
            UserService userService = getUserService();
            userService.Register(u, a);
            return u;
        }

        private ProductService getProductService()
        {
            GenericRepository<Product> repoInstance = new GenericRepository<Product>(getContext());
            GenericRepository<ProductFeature> productFeatureRepoInstance = new GenericRepository<ProductFeature>(getContext());
            GenericRepository<Feature> featureRepoInstance = new GenericRepository<Feature>(getContext());
            GenericRepository<OrderProduct> orderProductRepoInstance = new GenericRepository<OrderProduct>(getContext());
            GenericRepository<Review> reviewRepoInstance = new GenericRepository<Review>(getContext());
            GenericRepository<Order> orderRepoInstance = new GenericRepository<Order>(getContext());
            return new ProductService(repoInstance, productFeatureRepoInstance, featureRepoInstance, orderProductRepoInstance, reviewRepoInstance, orderRepoInstance);
        }
        private CategoryService getCategoryService()
        {
            GenericRepository<Category> repoInstance = new GenericRepository<Category>(getContext());
            GenericRepository<Product> pRepoInstance = new GenericRepository<Product>(getContext());
            return new CategoryService(repoInstance, pRepoInstance);
        }

        private Category getCategory()
        {
            Category c = new Category();
            c.Name = "Nombre Cat";
            c.Description = "Description Cat";
            CategoryService service = getCategoryService();
            service.Add(c);
            return c;
        }
        private Product generateProduct()
        {
            ProductService service = getProductService();
            Product p = new Product();
            p.Code = "1234";
            p.Description = "Desc";
            p.Manufacturer = "Manu";
            p.Name = "Name";
            p.Price = 200;
            p.Category = getCategory();
            service.Add(p);
            return p;
        }

        private Category getCategory2()
        {
            Category c2 = new Category();
            c2.Name = "Nombre Cat2";
            c2.Description = "Description Cat2";
            CategoryService service = getCategoryService();
            service.Add(c2);
            return c2;
        }
        private Product generateProduct2()
        {
            ProductService service = getProductService();
            Product p2 = new Product();
            p2.Code = "2222";
            p2.Description = "Desc";
            p2.Manufacturer = "Manu";
            p2.Name = "Name2";
            p2.Price = 600;
            p2.Category = getCategory2();
            service.Add(p2);
            return p2;
        }
        

        [TestMethod]
        public void RegisterUserCreateOrderOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();

            Order order = orderService.GetActiveOrderFromUser(u);
            Assert.IsNotNull(order);
            Assert.AreEqual(OrderStatuses.WAITING_FOR_ADDRESS, order.Status);
        }

        [TestMethod]
        public void AddProductOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            Order order = orderService.GetActiveOrderFromUser(u);
            ProductService productService = getProductService();

            List<Product> productsOfOrder = productService.GetAllFromOrder(order);
            Assert.AreEqual(productsOfOrder.Count, 1);
        }

        [TestMethod]
        public void AddProductWithNoOrderWaitingForAddressTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            orderService.SetAddress(u, u.Address.Id);
            orderService.AddProduct(u, p.Id);
            Order checkOrder = orderService.GetActiveOrderFromUser(u);
            Assert.IsNotNull(checkOrder);
            Assert.AreEqual(checkOrder.Status, OrderStatuses.WAITING_FOR_ADDRESS);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void AddProductNoUserTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            User u2 = new User();
            Guid id = Guid.NewGuid();
            u2.Id = id;
            Product p = generateProduct();
            orderService.AddProduct(u2, p.Id);
        }

        [ExpectedException(typeof(NotExistingProductException))]
        [TestMethod]
        public void AddProductNotExistingProductTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            Product p2 = new Product();
            Guid id = Guid.NewGuid();
            p2.Id = id;
            orderService.AddProduct(u, p2.Id);
        }

        [ExpectedException(typeof(InactiveProductException))]
        [TestMethod]
        public void AddProductInactiveTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            p.IsActive = false;
            orderService.AddProduct(u, p.Id);
        }

        [ExpectedException(typeof(WrongProductQuantityException))]
        [TestMethod]
        public void AddProductWrongQuantityTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            int quantity = -5;
            orderService.AddProduct(u, p.Id, quantity);
        }

        [TestMethod]
        public void AddProductTwiceQuantityOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            orderService.AddProduct(u, p.Id);
            Order order = orderService.GetActiveOrderFromUser(u);
            int ammount = orderService.GetQuantityOfProductInOrder(order, p.Id);

            Assert.AreEqual(ammount, 2);
        }

        //Delete

        [TestMethod]
        public void DeleteProductOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            orderService.DeleteProduct(u.Id, p.Id);
            Order order = orderService.GetActiveOrderFromUser(u);
            ProductService productService = getProductService();
            List<Product> productsOfOrder = productService.GetAllFromOrder(order);
            Assert.AreEqual(productsOfOrder.Count, 0);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void DeleteProductNoUserTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            User u2 = new User();
            Guid id = Guid.NewGuid();
            u2.Id = id;
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            orderService.DeleteProduct(u2.Id, p.Id);
        }

        [ExpectedException(typeof(NotExistingOrderException))]
        [TestMethod]
        public void DeleteProductNoOrderTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            Order order = orderService.GetActiveOrderFromUser(u);
            orderService.SetAddress(u, u.Address.Id);
            orderService.DeleteProduct(u.Id, p.Id);
        }

        [ExpectedException(typeof(NotExistingProductException))]
        [TestMethod]
        public void DeleteProductNoProductTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            Product p2 = new Product();
            Guid id = Guid.NewGuid();
            p2.Id = id;
            orderService.DeleteProduct(u.Id, p2.Id);
        }

        [ExpectedException(typeof(NotExistingProductInOrderException))]
        [TestMethod]
        public void DeleteProductNoExistingProductInOrderTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            Product p2 = new Product();
            p2.Code = "1235";
            p2.Description = "Desc";
            p2.Manufacturer = "Manu";
            p2.Name = "Name Product 2";
            p2.Price = 120;
            p2.Category = p.Category;
            ProductService productService = getProductService();
            productService.Add(p2);

            orderService.DeleteProduct(u.Id, p2.Id);
        }

        //Change Product Quantity

        [TestMethod]
        public void ChangeProductQuantityInOrderOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            int newQuantity = 3;
            orderService.ChangeProductQuantity(u, p.Id, newQuantity);
            Order order = orderService.GetActiveOrderFromUser(u);
            int ammount = orderService.GetQuantityOfProductInOrder(order, p.Id);
            Assert.AreEqual(newQuantity, ammount);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void ChangeProductQuantityInOrderNoUserTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            int newQuantity = 3;
            User u2 = new User();
            u2.Id = Guid.NewGuid();
            orderService.ChangeProductQuantity(u2, p.Id, newQuantity);
        }

        [ExpectedException(typeof(WrongProductQuantityException))]
        [TestMethod]
        public void ChangeProductQuantityInOrderWrongQuantityTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            int newQuantity = -5;
            orderService.ChangeProductQuantity(u, p.Id, newQuantity);
        }


        [ExpectedException(typeof(NotExistingProductInOrderException))]
        [TestMethod]
        public void ChangeProductQuantityInOrderNotExistingProductInOrderTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();

            ProductService service = getProductService();
            Product p2 = new Product();
            p2.Code = "1235";
            p2.Description = "Desc";
            p2.Manufacturer = "Manu";
            p2.Name = "Name2";
            p2.Price = 120;
            p2.Category = p.Category;
            service.Add(p2);

            orderService.AddProduct(u, p.Id);
            int newQuantity = 5;
            orderService.ChangeProductQuantity(u, p2.Id, newQuantity);
        }

        //View all products

        [TestMethod]
        public void ViewAllProductsWithOrderIdOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            List<Product> allProducts = orderService.ViewAllProductsFromOrder(u, orderId);
            Assert.AreEqual(allProducts.Count, 1);
        }

        [TestMethod]
        public void ViewAllProductsWithoutOrderIdOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            List<Product> allProducts = orderService.ViewAllProductsFromOrder(u);
            Assert.AreEqual(allProducts.Count, 1);
        }


        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void ViewAllProductsNoUserTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            User u2 = new User();
            u2.Id = Guid.NewGuid();
            List<Product> allProducts = orderService.ViewAllProductsFromOrder(u2);
        }

        [ExpectedException(typeof(NotExistingOrderException))]
        [TestMethod]
        public void ViewAllProductsNoExistingOrderTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            List<Product> allProducts = orderService.ViewAllProductsFromOrder(u, Guid.NewGuid());
        }

        [ExpectedException(typeof(NotExistingOrderException))]
        [TestMethod]
        public void ViewAllProductsNotMyOrderTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            Order o2 = new Order();
            o2.Id = Guid.NewGuid();
            List<Product> allProducts = orderService.ViewAllProductsFromOrder(u, o2.Id);
        }

        //Add address

        [TestMethod]
        public void AddAddressToOrderOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            orderService.SetAddress(u, u.Address.Id);

            Assert.AreEqual(order.AddressId, u.Address.Id);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void AddAddressToOrderNoUserTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            User u2 = new User();
            Guid id = Guid.NewGuid();
            u2.Id = id;
            orderService.SetAddress(u2, u.Address.Id);
        }

        [ExpectedException(typeof(NotExistingOrderException))]
        [TestMethod]
        public void AddAddressToOrderWithWrongStatusTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();

            orderService.AddProduct(u, p.Id);
            Order order = orderService.GetActiveOrderFromUser(u);
            order.Status = OrderStatuses.WAITING_FOR_DELIVERY;
            orderService.SetAddress(u, u.Address.Id);
        }

        //Ship

        [TestMethod]
        public void ShipOrderOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            orderService.SetAddress(u, u.Address.Id);
            orderService.Ship(orderId);
            Order o2 = orderService.Get(orderId);
            Assert.AreEqual(o2.Status, OrderStatuses.ON_ITS_WAY);
        }

        [ExpectedException(typeof(NotExistingOrderException))]
        [TestMethod]
        public void ShipOrderNoOrderTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            orderService.SetAddress(u, u.Address.Id);
            Order order = orderService.GetActiveOrderFromUser(u);
            Order o2 = new Order();
            Guid id = Guid.NewGuid();
            o2.Id = id;
            orderService.Ship(o2.Id);
        }

        [ExpectedException(typeof(NotExistingOrderWithCorrectStatusException))]
        [TestMethod]
        public void ShipOrderWithWrongStatusTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();

            orderService.AddProduct(u, p.Id);
            Guid orderId = orderService.GetActiveOrderFromUser(u).Id;
            orderService.SetAddress(u, u.Address.Id);
            Order o2 = orderService.Get(orderId);
            o2.Status = OrderStatuses.PAYED;
            orderService.Ship(o2.Id);
        }


        //Pay

        [TestMethod]
        public void ReceiveOrderPaymentOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            orderService.SetAddress(u, u.Address.Id);
            orderService.Ship(orderId);
            orderService.Pay(orderId);
            Order o2 = orderService.Get(orderId);
            Assert.AreEqual(o2.Status, OrderStatuses.PAYED);
        }

        [ExpectedException(typeof(NotExistingOrderException))]
        [TestMethod]
        public void ReceiveOrderPaymentNoOrderTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            Guid orderId = orderService.GetActiveOrderFromUser(u).Id;
            orderService.AddProduct(u, p.Id);
            orderService.SetAddress(u, u.Address.Id);
            orderService.Ship(orderId);
            Guid wrongId = Guid.NewGuid();
            orderService.Pay(wrongId);
        }

        [ExpectedException(typeof(NotExistingOrderWithCorrectStatusException))]
        [TestMethod]
        public void ReceiveOrderPaymentOrderWithWrongStatusTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);
            Guid orderId = orderService.GetActiveOrderFromUser(u).Id;
            orderService.SetAddress(u, u.Address.Id);
            orderService.Ship(orderId);
            Order o2 = orderService.Get(orderId);
            o2.Status = OrderStatuses.FINALIZED;
            orderService.Pay(o2.Id);

        }

        //Cancel

        [TestMethod]
        public void CancelOrderByUserOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            orderService.SetAddress(u, u.Address.Id);
            orderService.Cancel(u, order.Id);
            Assert.AreEqual(order.Status, OrderStatuses.CANCELLED_BY_USER);
        }

        [TestMethod]
        public void CancelOrderByStoreOkTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            orderService.SetAddress(u, u.Address.Id);
            orderService.Ship(orderId);

            User admin = new User();
            admin.FirstName = "Martin";
            admin.LastName = "Musetti";
            admin.PhoneNumber = "+59899211266";
            admin.Password = "prueba12345";
            admin.Email = "martin@gmail.com";
            admin.Username = "Martin";
            admin.Role = UserRoles.ADMIN;
            Address a2 = new Address();
            a2.Street = "Miami";
            a2.StreetNumber = "1766";
            a2.PhoneNumber = "26002540";
            UserService userService = getUserService();
            userService.Register(admin, a2);
            admin.Role = UserRoles.ADMIN;
            orderService.Cancel(admin, orderId);
            Assert.AreEqual(order.Status, OrderStatuses.CANCELLED_BY_STORE);
        }

        [ExpectedException(typeof(NotExistingUserException))]
        [TestMethod]
        public void CancelOrderByUserNoUserTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            orderService.SetAddress(u, u.Address.Id);
            User u2 = new User();
            u2.Id = Guid.NewGuid();
            orderService.Cancel(u2, order.Id);
        }

        [ExpectedException(typeof(NotExistingOrderException))]
        [TestMethod]
        public void CancelOrderNoOrderTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            orderService.SetAddress(u, u.Address.Id);
            Order o2 = new Order();
            o2.Id = Guid.NewGuid();
            orderService.Cancel(u, o2.Id);
        }

        [ExpectedException(typeof(IncorrectOrderStatusException))]
        [TestMethod]
        public void CancelOrderByUserIncorrectOrderStatusTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            orderService.SetAddress(u, u.Address.Id);
            orderService.Ship(orderId);
            orderService.Cancel(u, order.Id);
        }

        [ExpectedException(typeof(IncorrectOrderStatusException))]
        [TestMethod]
        public void CancelOrderByStoreIncorrectOrderStatusTest()
        {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            orderService.SetAddress(u, u.Address.Id);
            orderService.Ship(orderId);
            orderService.Pay(orderId);
            User admin = new User();
            admin.FirstName = "Martin";
            admin.LastName = "Musetti";
            admin.PhoneNumber = "+59899211266";
            admin.Password = "prueba12345";
            admin.Email = "martin@gmail.com";
            admin.Username = "Martin";
            admin.Role = UserRoles.ADMIN;
            Address a2 = new Address();
            a2.Street = "Miami";
            a2.StreetNumber = "1766";
            a2.PhoneNumber = "26002540";
            UserService userService = getUserService();
            userService.Register(admin, a2);
            admin.Role = UserRoles.ADMIN;
            orderService.Cancel(admin, orderId);
        }

        [TestMethod]
        public void GetCategoryStatisticsTest() {
            OrderService orderService = getOrderService();
            User u = registerUser();
            Product p = generateProduct();
            Product p2 = generateProduct2();

            orderService.AddProduct(u, p.Id);
            orderService.AddProduct(u, p2.Id);
            orderService.AddProduct(u, p.Id);

            Order order = orderService.GetActiveOrderFromUser(u);
            Guid orderId = order.Id;
            orderService.SetAddress(u, u.Address.Id);
            orderService.Ship(orderId);
            orderService.Pay(orderId);

            DateTime tomorrow = DateTime.Now.AddDays(1);
            DateTime yesterday = DateTime.Now.AddDays(-1);

            List<string> categoryStatistics = orderService.GetCategoryStatistics(yesterday, tomorrow);

            Assert.AreEqual(4, categoryStatistics.Count);
            Assert.AreEqual("Nombre Cat2 - 60% - $600", categoryStatistics[0]);
            Assert.AreEqual("Nombre Cat - 40% - $400", categoryStatistics[1]);
            Assert.AreEqual("------------------------", categoryStatistics[2]);
            Assert.AreEqual("Total ($) - $1000", categoryStatistics[3]);
        }


    }
}
