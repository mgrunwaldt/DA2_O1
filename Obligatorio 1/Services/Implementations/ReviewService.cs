using Entities;
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

        public ReviewService(GenericRepository<Review> reviewRepo, GenericRepository<Product> productRepo)
        {
            this.reviewRepository = reviewRepo;
            this.productRepository = productRepo;
        }

        public void Evaluate(User u, Guid id, Guid orderId, string reviewText)
        {
            throw new NotImplementedException();
        }

        public Review Get(Guid id, Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}