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
    }
}