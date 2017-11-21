using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Data
{
    public interface IProductReviewRepository : IRepository<ProductReview>
    {
        IEnumerable<ProductReview> ListByProductId(int id);

        ProductReview GetByProductAndUserIds(int productId, string userId);
        
    }
}
