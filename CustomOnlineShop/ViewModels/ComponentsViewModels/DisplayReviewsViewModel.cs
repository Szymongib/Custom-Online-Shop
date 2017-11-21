using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class DisplayReviewsViewModel
    {
        public ProductReviewViewModel CurrentUserReview { get; set; }
        public IEnumerable<ProductReviewViewModel> OtherUsersReviews { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
