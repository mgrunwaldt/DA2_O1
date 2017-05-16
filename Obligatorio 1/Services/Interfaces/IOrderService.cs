using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Order GetActiveOrderFromUser(User u);
        void AddProduct(User u, Guid pId, int quantity = 1);
        void DeleteProduct(Guid uId, Guid pId);
        void ChangeProductQuantity(User u, Guid productId, int newQuantity);
        List<Product> ViewAllProductsFromOrder(User u, object orderIdObj = null);
        void SetAddress(User u, Guid id);
        void Ship(Guid orderId);
        Order Get(Guid orderId);
        void Pay(Guid orderId);
        void Cancel(User u, Guid orderId);
    }
}