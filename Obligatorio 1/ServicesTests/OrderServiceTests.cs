using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Statuses_And_Roles;

namespace ServicesTests
{
    [TestClass]
    class OrderServiceTests
    {
        private UserService getUserService()
        {
            GenericRepository<User> repo = new GenericRepository<User>();
            return new UserService(repo);
        }

        private OrderService getOrderService()
        {
            GenericRepository<Order> repo = new GenericRepository<Order>(true);
            return new OrderService(repo);
        }

        [TestMethod]
        public void RegisterUserCreateOrderOkTest()
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
            OrderService orderService = getOrderService();
            UserService userService = getUserService();
            
            userService.Register(u, a);

            Order order = orderService.GetActiveOrderFromUser(u);
            Assert.IsNotNull(order);
            Assert.AreEqual(OrderStatuses.WAITING_FOR_ADDRESS, order.Status);
        }
        //Create User, new empty order

        //ADD PRODUCT:
        //ok
        //Create Order if no status = in proccess
        //No user
        //no product
        //existing product not active
        //wrong product
        //no quantity
        //wrong quantity
        //Same product added twice, add quantity 

        //DELETE PRODUCT FROM ORDER
        //OK
        //No user
        //No active order
        //No product
        //No product in order

        //CHANGE PRODUCT QUANTITY
        //OK
        //No user
        //No active order
        //Wrong quantity
        //No product in order

        //VIEW ALL PRODUCTS
        //OK Without Order Id
        //OK With Order Id
        //No user
        //Not Existing Order Id
        //Not mine order Id

        //ADD ADDRESS
        //OK
        //ORDER STATUS != WAITING FOR ADDRESS
        //No Address for user
        //No order with status waiting for address
        //No user

        //SHIP
        //OK
        //NO USER
        //USER ROLE != ADMIN
        //NO ORDER
        //ORDER STATUS != FROM WAITING FOR DELIVERY

        //RECEIVE PAYMENT
        //OK
        //NO USER
        //USER ROLE != ADMIN
        //NO ORDER
        //ORDER STATUS != FROM WAITING FOR DELIVERY

        //CANCEL
        //USER OK (puede hasta en camino)
        //ADMIN OK (puede hasta pago)
        //NO USER
        //NO ORDER
        //ORDER STATUS WRONG USER
        //ORDER STATUS WRONG ADMIN
        //USER NOT ADMIN ORDER NOT HIS







    }
}
