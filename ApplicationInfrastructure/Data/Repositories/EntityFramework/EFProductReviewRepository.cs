using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;

namespace ApplicationInfrastructure.Data.Repositories
{
    public class EFProductReviewRepository : EFBaseRepository<ProductReview>, IProductReviewRepository
    {
        public EFProductReviewRepository(OnlineShopDbContext context) : base(context)
        {   }

        public IEnumerable<ProductReview> ListByProductId(int id)
        {
            return _context.ProductReviews.Where(x => x.ProductID == id).AsEnumerable();
        }

        public ProductReview GetByProductAndUserIds(int productId, string userId)
        {
            return _context.ProductReviews.FirstOrDefault(x => x.ProductID == productId && x.AuthorID == userId);
        }
    }
}
