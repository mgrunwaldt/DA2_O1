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
            User user = userRepo.Get(u.Id);
            if (user != null){
                Order order = this.GetActiveOrderFromUser(u);
                if (order != null)
                {
                    Product prod = productRepo.Get(p.Id);
                    if (prod != null)
                    {
                        List<OrderProduct> allOrderProducts = orderProductRepo.GetAll();
                        OrderProduct orderProd = allOrderProducts.Find(op => op.OrderId == order.Id && op.ProductId == p.Id);
                        if (orderProd != null)
                        {
                            orderProductRepo.Delete(orderProd);
                        }
                        else
                        {
                            throw new NotExistingProductInOrderException("No existe el producto en la orden indicada");
                        }
                    }
                    else
                    {
                        throw new NotExistingProductException("No existe el producto indicado");
                    }
                }
                else
                {
                    throw new NotExistingOrderException("No hay una order esperando direccion para este usuario");
                }
            }else
            {
                throw new NotExistingUserException("No existe el usuario");
            }
        }

        public void ChangeProductQuantity(User u, Guid productId, int newQuantity)
        {
            User user = userRepo.Get(u.Id);
            if (user != null)
            {
                Order order = this.GetActiveOrderFromUser(u);
                if (order != null)
                {
                    List<OrderProduct> allOrderProducts = orderProductRepo.GetAll();
                    OrderProduct orderProd = allOrderProducts.Find(op => op.OrderId == order.Id && op.ProductId == productId);
                    if(orderProd != null)
                    {
                        if (newQuantity > 0)
                        {
                            orderProd.Quantity = newQuantity;
                            orderProductRepo.Update(orderProd);
                        }
                        else
                        {
                            throw new WrongProductQuantityException("La cantidad ingresada no es válida");
                        }
                    }else
                    {
                        throw new NotExistingProductInOrderException("No existe el producto indicado para esa orden");
                    }
                }
                else
                {
                    throw new NotExistingOrderException("No hay una orden esperando direccion para este usuario");
                }
            }
            else
            {
                throw new NotExistingUserException("No existe el usuario");
            }
        }

        public List<Product> ViewAllProductsFromOrder(User u, Object orderIdObj = null)
        {
            User user = userRepo.Get(u.Id);
            if (user != null)
            {
                List<Product> ret = new List<Product>();
                List<OrderProduct> allOrderProducts = orderProductRepo.GetAll();
                if (orderIdObj != null)
                {
                    Guid orderId = (Guid)orderIdObj;
                    Order order = orderRepo.Get(orderId);
                    if (order != null)
                    {
                        if (order.UserId == user.Id)
                        {
                            foreach (var orderProduct in allOrderProducts)
                            {
                                if (orderProduct.OrderId == orderId)
                                {
                                    Product p = productRepo.Get(orderProduct.ProductId);
                                    ret.Add(p);
                                }
                            }
                        }else
                        {
                            throw new NotExistingOrderException("Esta orden no pertenece al usuario especificado");
                        }
                    }
                    else
                    {
                        throw new NotExistingOrderException("No existe la orden");
                    }
                }
                else
                {
                    Order order = this.GetActiveOrderFromUser(u);
                    if (order != null)
                    {
                        foreach (var orderProduct in allOrderProducts)
                        {
                            if (orderProduct.OrderId == order.Id)
                            {
                                Product p = productRepo.Get(orderProduct.ProductId);
                                ret.Add(p);
                            }
                        }
                    }
                    else
                    {
                        throw new NotExistingOrderException("No hay una orden esperando dirección para este usuario");
                    }
                }
                return ret;
            }else
            {
                throw new NotExistingUserException("No existe el usuario");
            }
        }

        public void SetAddress(User u, Guid id)
        {
            User user = userRepo.Get(u.Id);
            if (user != null){
                List<Order> allOrders = orderRepo.GetAll();
                Order order = allOrders.Find(o => o.UserId == u.Id && o.Status == OrderStatuses.WAITING_FOR_ADDRESS);
                if (order != null)
                {
                    bool isOk = false;
                    List<OrderProduct> allOrderProducts = orderProductRepo.GetAll();
                    foreach (var orderProduct in allOrderProducts)
                    {
                        if (orderProduct.OrderId == order.Id)
                        {
                            isOk = true;
                        }
                    }
                    if (isOk)
                    {
                        order.AddressId = id;
                        order.Status = OrderStatuses.WAITING_FOR_DELIVERY;
                        orderRepo.Update(order);
                    }
                    else
                    {
                        throw new NotExistingProductInOrderException("La orden no tiene productos");
                    }
                }
                else
                {
                    throw new NotExistingOrderException("No existe orden esperando por dirección para el usuario indicado");
                }
            }else{
                throw new NotExistingUserException("No existe el usuario");
            }
            
        }

        public void Ship(Guid orderId)
        {
            Order order = orderRepo.Get(orderId);
            if (order != null)
            {
                int orderStatus = order.Status;
                if (orderStatus == OrderStatuses.WAITING_FOR_DELIVERY)
                {
                    order.Status = OrderStatuses.ON_ITS_WAY;
                    orderRepo.Update(order);
                }else
                {
                    throw new NotExistingOrderWithCorrectStatusException("La orden indicada no esta en estado 'Esperando para ser entregada'");
                }
            }else
            {
                throw new NotExistingOrderException("No existe la orden");
            }
        }

        public Order Get(Guid orderId)
        {
            Order order = orderRepo.Get(orderId);
            if (order != null)
            {
                return order;
            }else
            {
                throw new NotExistingOrderException("No existe la orden");
            }
        }

        public void Pay(Guid orderId)
        {
            Order order = orderRepo.Get(orderId);
            if (order != null)
            {
                int orderStatus = order.Status;
                if (orderStatus == OrderStatuses.ON_ITS_WAY)
                {
                    order.Status = OrderStatuses.PAYED;
                    orderRepo.Update(order);
                }
                else
                {
                    throw new NotExistingOrderWithCorrectStatusException("La orden indicada no esta en estado 'Esperando para ser entregada'");
                }
            }
            else
            {
                throw new NotExistingOrderException("No existe la orden");
            }
        }

        public void Cancel(User u, Guid orderId)
        {
            User user = userRepo.Get(u.Id);
            if (user != null)
            {
                Order order = orderRepo.Get(orderId);
                if (order != null)
                {
                    int orderStatus = order.Status;
                    if (user.Role == UserRoles.USER)
                    {
                        if (orderStatus == OrderStatuses.WAITING_FOR_ADDRESS || orderStatus == OrderStatuses.WAITING_FOR_DELIVERY)
                        {
                            order.Status = OrderStatuses.CANCELLED_BY_USER;
                            orderRepo.Update(order);
                        }else
                        {
                            throw new IncorrectOrderStatusException("No se puede cancelar esta orden por que ya esta en camino a ser enviada");
                        }
                    }else
                    {
                        if (orderStatus == OrderStatuses.WAITING_FOR_ADDRESS || orderStatus == OrderStatuses.WAITING_FOR_DELIVERY || orderStatus == OrderStatuses.ON_ITS_WAY)
                        {
                            order.Status = OrderStatuses.CANCELLED_BY_STORE;
                            orderRepo.Update(order);
                        }
                        else
                        {
                            throw new IncorrectOrderStatusException("No se puede cancelar esta orden por que ya esta en camino a ser enviada");
                        }
                    }
                }else
                {
                    throw new NotExistingOrderException("No existe la orden");
                }
            }
            else
            {
                throw new NotExistingUserException("No existe el usuario");
            }
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