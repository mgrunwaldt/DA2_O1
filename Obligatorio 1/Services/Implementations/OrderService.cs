using System;
using Entities;
using Repository;
using Services.Interfaces;
using System.Collections.Generic;
using Entities.Statuses_And_Roles;
using Exceptions;

namespace Services
{
    public class OrderService: IOrderService
    {
        private GenericRepository<Order> repo;

        public OrderService(GenericRepository<Order> repo)
        {
            this.repo = repo;
        }

        public Order GetActiveOrderFromUser(User u)
        {
            List<Order> allOrders = repo.GetAll();
            Order order = allOrders.Find(o => o.UserId == u.Id && o.Status == OrderStatuses.WAITING_FOR_ADDRESS);
            if( order == null)
            {
                throw new UserWithoutActiveOrderException("Este usuario no tiene una orden activa");
            }
            return order;
        }

        public void AddProduct(User u2, Product p, int quantity = 0)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(User u, Product p)
        {
            throw new NotImplementedException();
        }

        public void ChangeProductQuantity(User u, Guid id, int newQuantity)
        {
            throw new NotImplementedException();
        }

        public List<Product> ViewAllProductsFromOrder(User u, Guid orderId)
        {
            throw new NotImplementedException();
        }

        public List<Product> ViewAllProductsFromActiveOrder(User u)
        {
            throw new NotImplementedException();
        }

        public void SetAddress(User u, Guid id)
        {
            throw new NotImplementedException();
        }

        public void Ship(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Order Get(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public void Pay(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public void Cancel(User u, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}