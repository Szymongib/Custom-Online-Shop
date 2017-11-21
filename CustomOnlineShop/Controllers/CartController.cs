using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using ApplicationInfrastructure.Identity;
using CustomOnlineShop.Services.Interfaces;
using CustomOnlineShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.Controllers
{
    public class CartController : Controller
    {
        UserManager<AppUser> _userManager;
        IProductRepository _productRepository;
        ICartService _cartService;

        #region Constructor

        public CartController(UserManager<AppUser> userManager, IProductRepository productRepository, ICartService cartSrvice)
        {
            _userManager = userManager;
            _productRepository = productRepository;
            _cartService = cartSrvice;
        }

        #endregion
        

        [HttpPost]
        public IActionResult AddToCart(CatalogItemViewModel model)
        {
            _cartService.AddToCart(model);

            return RedirectToAction("Index");
        }
        
        public ViewResult Index()
        {
            CartIndexViewModel model = CreateCartIndexViewModel();
            
            return View(model);
        }

        public IActionResult EditItem(CartItemViewModel model)
        {
            _cartService.UpdateCartEntry(model.ProductId, model.Quantity);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveItem(CartItemViewModel model)
        {
            _cartService.RemoveFromCart(model);

            return RedirectToAction("Index");
        }

        public IActionResult GoToCheckout()
        {
            if (_cartService.IsEmpty())
            {
                ModelState.AddModelError("Cart is empty", "Cart can not be empty!");
                return View("Index", CreateCartIndexViewModel());
            }
            else
            {
                return RedirectToAction("Index", "Checkout");
            }
        }



        private CartIndexViewModel CreateCartIndexViewModel()
        {
            Cart cart = _cartService.GetOrCreateCart();
            CartIndexViewModel model = new CartIndexViewModel();

            foreach (CartEntry entry in cart.CartEntries)
            {
                Product p = _productRepository.GetById(entry.ProductID);
                model.CartItems.Add(new CartItemViewModel
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductPrice = p.Price,
                    Quantity = entry.Quantity
                });
            }

            return model;
        }

    }
}
