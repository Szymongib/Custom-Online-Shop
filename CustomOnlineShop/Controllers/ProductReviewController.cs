using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using ApplicationInfrastructure.Identity;
using CustomOnlineShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.Controllers
{
    public class ProductReviewController : Controller
    {

        IProductRepository _productRepository;
        UserManager<AppUser> _userManager;
        IProductReviewRepository _productReviewRepository;
        IOrderRepository _orderRepository;

        #region Constructor

        public ProductReviewController(
            IProductRepository productRepository,
            UserManager<AppUser> userManager,
            IProductReviewRepository productReviewRepository,
            IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _userManager = userManager;
            _productReviewRepository = productReviewRepository;
            _orderRepository = orderRepository;
        }

        #endregion


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateReview(int productId)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            ProductReview review = _productReviewRepository.GetByProductAndUserIds(productId, user.Id);

            if(review != null)
            {
                return View("UnableToCreateReview", Constants.ProductAlreadyReviewd);
            }

            List<Order> userOrders = _orderRepository.ListAllByUserId(user.Id).ToList();

            List<OrderEntry> entries = new List<OrderEntry>();
            userOrders.ForEach(x => entries.AddRange(_orderRepository.ListEntriesByOrderId(x.Id)));

            bool productFound = false;

            foreach (OrderEntry entry in entries)
            {
                if(entry.ProductID == productId)
                {
                    productFound = true;
                    break;
                }
            }

            if (!productFound)
            {
                return View("UnableToCreateReview", Constants.ProductNotOrdered);
            }

            return View();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReview(ProductReviewViewModel model)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                ProductReview productReview = new ProductReview();

                productReview.AuthorID = user.Id;
                productReview.ProductID = model.ProductId;
                productReview.PostDate = DateTime.Now;
                productReview.Title = model.Title;
                productReview.Description = model.Description;
                productReview.Rating = model.Rating;
                productReview.AuthorUserName = user.UserName;

                _productReviewRepository.Add(productReview);

                return RedirectToAction("Item", "Catalog", new { productId = model.ProductId });
            }

            return View(model);
        }

        [Authorize]
        public IActionResult DeleteReview(int reviewId)
        {
            ProductReview review = _productReviewRepository.GetById(reviewId);
            int itemId = review.ProductID;
            _productReviewRepository.Delete(review);

            return RedirectToAction("Item", "Catalog", new { productId = itemId });
        }

    }
}
