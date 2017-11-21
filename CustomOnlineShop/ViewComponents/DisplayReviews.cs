using ApplicationCore.Interfaces.Data;
using ApplicationInfrastructure.Identity;
using CustomOnlineShop.Helpers;
using CustomOnlineShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewComponents
{

    public class DisplayReviews : ViewComponent
    {
        UserManager<AppUser> _userManager;
        IProductReviewRepository _productReviewRepository;

        #region Constructor

        public DisplayReviews(UserManager<AppUser> userManager, IProductReviewRepository productReviewRepository)
        {
            _userManager = userManager;
            _productReviewRepository = productReviewRepository;
        }

        #endregion

        public IViewComponentResult Invoke(int productId, string userName)
        {
            DisplayReviewsViewModel model = new DisplayReviewsViewModel();

            IEnumerable<ProductReviewViewModel> reviews = _productReviewRepository.ListByProductId(productId).Select(x => x.MapToViewModel());

            if (userName != null)
            {
                model.CurrentUserReview = reviews.FirstOrDefault(x => x.AuthorUserName == userName);
                model.OtherUsersReviews = reviews.Where(x => x.AuthorUserName != userName);
            }
            else
            {
                model.OtherUsersReviews = reviews;
            }

            return View(model);
        }


    }
}
