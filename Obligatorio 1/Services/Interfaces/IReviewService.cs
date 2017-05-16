using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Services.Interfaces
{
    public interface IReviewService
    {
        void Evaluate(User u, Guid productId, Guid orderId, string reviewText);
    }
}