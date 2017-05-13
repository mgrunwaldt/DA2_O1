using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesTests
{
    [TestClass]
    class OrderServiceTests
    {
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
