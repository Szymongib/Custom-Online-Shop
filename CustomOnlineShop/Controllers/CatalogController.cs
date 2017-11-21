using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using ApplicationInfrastructure.Identity;
using CustomOnlineShop.Helpers;
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

    [Route("")]
    public class CatalogController : Controller
    {
        IProductRepository _productRepository;
        IProductCategoryRepository _productCategoryRepository;
        UserManager<AppUser> _userManager;
        IProductReviewRepository _productReviewRepository;

        #region Constructor

        public CatalogController(
            IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository,
            UserManager<AppUser> userManager,
            IProductReviewRepository productReviewRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _userManager = userManager;
            _productReviewRepository = productReviewRepository;
        }

        #endregion

        [Route("")]
        public ViewResult Index(int pageIndex, int? categoryId, string searched)
        {
            CatalogIndexViewModel model = GetItems(pageIndex, categoryId, searched);

            return View(model);
        }

        [Route("/Item")]
        public ViewResult Item(int productId)
        {
            Product product = _productRepository.GetById(productId);
            product.ProductReviews = _productReviewRepository.ListByProductId(product.Id).ToList();

            CatalogItemViewModel model = product.MapToViewModel();

                
            return View(model);
        }

        [Route("/Error")]
        public ViewResult Error()
        {
            return View();
        }


        private CatalogIndexViewModel GetItems(int pageIndex, int? categoryId, string searched)
        {
            int itemsPerPage = Constants.ItemsPerPage;

            // Get all items of selected category, if null get all products
            List<Product> products = categoryId != null && categoryId != 0 ? _productRepository.ListByCategoryId((int)categoryId).ToList() : _productRepository.ListAll().ToList();
            products.ForEach(x => x.ProductReviews = _productReviewRepository.ListByProductId(x.Id).ToList());

            if(searched != null)
            {
                products = products.Where(x => x.Name.ToLower().Contains(searched.ToLower()) == true).ToList();
            }

            List<CatalogItemViewModel> itemsOnPage = products.Skip(pageIndex * itemsPerPage).Take(itemsPerPage).Select(x => x.MapToViewModel()).ToList();

            CatalogIndexViewModel vm = new CatalogIndexViewModel
            {
                Items = itemsOnPage,
                Categories = _productCategoryRepository.ListAll().Select(x => x.MapToViewModel()),
                CurrentPage = pageIndex,
                SelectedCategoryId = categoryId ?? 0,
                PagesCount = int.Parse(Math.Ceiling(((decimal)products.Count() / itemsPerPage)).ToString()),
                SearchedPhrase = searched ?? ""
            };

            return vm;
        }

    }
}
