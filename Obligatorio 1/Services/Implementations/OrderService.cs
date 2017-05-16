using System;
using Entities;
using Repository;
using Services.Interfaces;
using System.Collections.Generic;
using Entities.Statuses_And_Roles;
using Exceptions;

namespace Services
{
    public class OrderService : IOrderService
    {
        private GenericRepository<Order> orderRepo;
        private GenericRepository<OrderProduct> orderProductRepo;
        private GenericRepository<User> userRepo;
        private GenericRepository<Product> productRepo;

        public OrderService(GenericRepository<Order> orderRepo, GenericRepository<OrderProduct> orderProductRepo, GenericRepository<User> userRepo, GenericRepository<Product> productRepo)
        {
            this.orderRepo = orderRepo;
            this.orderProductRepo = orderProductRepo;
            this.userRepo = userRepo;
            this.productRepo = productRepo;
        }

        public Order GetActiveOrderFromUser(User u)
        {
            List<Order> allOrders = orderRepo.GetAll();
            Order order = allOrders.Find(o => o.UserId == u.Id && o.Status == OrderStatuses.WAITING_FOR_ADDRESS);
            if( order == null)
            {
                Order newOrder = new Order();
                newOrder.Status = OrderStatuses.WAITING_FOR_ADDRESS;
                newOrder.UserId = u.Id;
                orderRepo.Add(newOrder);
            }
            return order;
        }

        public void AddProduct(User u, Product p, int quantity = 1)
        {
            User user = userRepo.Get(u.Id); 
            if(user != null){ 
                Order order = this.GetActiveOrderFromUser(u);
                if(order != null){
                    Product product = productRepo.Get(p.Id);
                    if (product != null)
                    {
                        if (product.IsActive)
                        {
                            if (quantity > 0)
                            {
                                List<OrderProduct> allOrderProducts = orderProductRepo.GetAll();
                                OrderProduct orderProd = allOrderProducts.Find(op => op.OrderId == order.Id && op.ProductId == product.Id);
                                if(orderProd == null)
                                {
                                    OrderProduct newOrderProduct = new OrderProduct();
                                    newOrderProduct.OrderId = order.Id;
                                    newOrderProduct.ProductId = p.Id;
                                    newOrderProduct.Quantity = quantity;
                                    orderProductRepo.Add(newOrderProduct);
                                }else
                                {
                                    orderProd.Quantity = orderProd.Quantity + 1;
                                    orderProductRepo.Update(orderProd);
                                }
                            }else
                            {
                                throw new WrongProductQuantityException("La cantidad ingresada no es válida.");
                            }
                        }else
                        {
                            throw new InactiveProductException("El producto indicado no esta activo");
                        }
                    }else
                    {
                        throw new NotExistingProductException("No existe el producto indicado");
                    }
                    
                }else
                {
                    Order newOrder = new Order();
                    newOrder.Status = OrderStatuses.WAITING_FOR_ADDRESS;
                    newOrder.UserId = u.Id;
                    orderRepo.Add(newOrder);
                }
            }
            else
            {
                throw new NotExistingUserException("No existe el usuario indicado");
            }
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
            List<Order> allOrders = orderRepo.GetAll();
            Order order = allOrders.Find(o => o.UserId == u.Id && o.Status == OrderStatuses.WAITING_FOR_ADDRESS);
            if (order != null)
            {
                order.AddressId = id;
                order.Status = OrderStatuses.WAITING_FOR_DELIVERY;
                orderRepo.Update(order);
            }else
            {
                throw new NotExistingOrderException("No existe orden esperando por dirección para el usuario indicado");
            }
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

        public int GetQuantityOfProductInOrder(Order order, Guid productId)
        {
            List<OrderProduct> allOrderProducts = orderProductRepo.GetAll();
            OrderProduct orderProd = allOrderProducts.Find(op => op.OrderId == order.Id && op.ProductId == productId);
            if(orderProd != null)
            {
                return orderProd.Quantity;
            }else
            {
                throw new NotExistingProductInOrderException("El producto o la orden no existen, o hoy una orden con el producto indicado");
            }
        }
    }
}