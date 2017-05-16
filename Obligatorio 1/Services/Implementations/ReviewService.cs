using Entities;
using Entities.Statuses_And_Roles;
using Exceptions;
using Repository;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ReviewService: IReviewService
    {
        private GenericRepository<Review> reviewRepository;
        private GenericRepository<Product> productRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<OrderProduct> orderProductRepository;

        public ReviewService(GenericRepository<Review> reviewRepo, GenericRepository<Product> productRepo, GenericRepository<User> userRepo, GenericRepository<Order> orderRepo, GenericRepository<OrderProduct> orderProductRepo)
        {
            this.reviewRepository = reviewRepo;
            this.productRepository = productRepo;
            this.userRepository = userRepo;
            this.orderRepository = orderRepo;
            this.orderProductRepository = orderProductRepo;
        }

        public void Evaluate(User u, Guid productId, Guid orderId, string reviewText)
        {
            User user = userRepository.Get(u.Id);
            if(user != null)
            {
                Product product = productRepository.Get(productId);
                if (product != null)
                {
                    Order order = orderRepository.Get(orderId);
                    if(order != null)
                    {
                        if(reviewText.Trim().Length > 0)
                        {
                            List<OrderProduct> allOrderProducts = orderProductRepository.GetAll();
                            OrderProduct orderProd = allOrderProducts.Find(op => op.OrderId == order.Id && op.ProductId == productId);
                            if (orderProd != null)
                            {
                                int orderStatus = orderRepository.Get(orderId).Status;
                                if(orderStatus == OrderStatuses.PAYED)
                                {
                                    List<Review> allReviews = reviewRepository.GetAll();
                                    bool exists = false;
                                    foreach(var review in allReviews)
                                    {
                                        if (review.OrderId == orderId && review.ProductId == productId)
                                        {
                                            exists = true;
                                        }
                                    }
                                    if (!exists)
                                    {
                                        Review rev = new Review();
                                        rev.OrderId = orderId;
                                        rev.ProductId = productId;
                                        rev.UserId = user.Id;
                                        rev.Text = reviewText;
                                        rev.Date = DateTime.Now;
                                        reviewRepository.Add(rev);
                                        List<Review> savedReviews = reviewRepository.GetAll();
                                        List<Review> orderReviews = savedReviews.FindAll(r => r.OrderId == orderId);
                                        List<OrderProduct> productsFromOrder = allOrderProducts.FindAll(op => op.OrderId == orderId);
                                        if(orderReviews.Count == productsFromOrder.Count)
                                        {
                                            order.Status = OrderStatuses.FINALIZED;
                                            orderRepository.Update(order);
                                        }
                                    }
                                    else
                                    {
                                        throw new ProductAlreadyEvaluatedException("El producto ya fue evaluado");
                                    }
                                }else
                                {
                                    throw new IncorrectOrderStatusException("La orden no esta habilitada para evaluar");
                                }
                            }else
                            {
                                throw new NotExistingProductInOrderException("El producto no esta en la orden");
                            }
                        }
                        else
                        {
                            throw new NoTextForReviewException("Debe ingresar un texto para evaluar este producto");
                        }
                    }else
                    {
                        throw new NotExistingOrderException("No existe esa orden");
                    }
                }else
                {
                    throw new NotExistingProductException("No existe ese producto");
                }
            }else
            {
                throw new NotExistingUserException("No existe el usuario");
            }
        }

        public Review Get(Guid productId, Guid orderId)
        {
            List<Review> savedReviews = reviewRepository.GetAll();
            Review rev = savedReviews.Find(r => r.OrderId == orderId && r.ProductId == productId);
            return rev;
        }
    }
}