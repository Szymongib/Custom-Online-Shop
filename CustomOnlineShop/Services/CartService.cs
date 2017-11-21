using ApplicationCore.Interfaces.Data;
using CustomOnlineShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using CustomOnlineShop.ViewModels;

namespace CustomOnlineShop.Services
{
    public class CartService : ICartService
    {
        protected ICartRepository _cartRepository;
        ICookieService _cookieService;
        IProductRepository _productRepository;

        #region Constructor

        public CartService(ICartRepository cartRepository, ICookieService cookieService, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _cookieService = cookieService;
            _productRepository = productRepository;
        }

        #endregion

        public virtual int GetCartId()
        {
            return GetCartIdFromCookie();
        }

        public virtual Cart GetOrCreateCart()
        {
            int cartId = GetCartIdFromCookie();
            Cart cart;

            if (cartId == 0)
            {
                cart = new Cart();
                cart = _cartRepository.Add(cart);
                SaveCartIdCookie(cart.Id);
                return cart;
            }

            cart = _cartRepository.GetById(cartId);

            cart.CartEntries = _cartRepository.ListEntriesByCartId(cart.Id).ToList();

            return cart;
        }

        public void AddToCart(CatalogItemViewModel model)
        {
            Cart cart = GetOrCreateCart();
            bool added = false;

            foreach (CartEntry cartEntry in cart.CartEntries)
            {
                if(cartEntry.ProductID == model.Id)
                {
                    cartEntry.Quantity += 1;
                    added = true;
                    break;
                }
            }

            if(added == false)
            {
                CartEntry entry = new CartEntry
                {
                    ProductID = model.Id,
                    CartID = cart.Id,
                    Quantity = 1
                };
                cart.CartEntries.Add(entry);
            }
            
            _cartRepository.Update(cart);
        }       

        public void UpdateCartEntry(int productId, int quantity)
        {
            Cart cart = GetOrCreateCart();

            foreach (CartEntry cartEntry in cart.CartEntries)
            {
                if (cartEntry.ProductID == productId)
                {
                    cartEntry.Quantity = quantity;
                    _cartRepository.Update(cart);
                    return;
                }
            }
        }

        public void RemoveFromCart(CartItemViewModel item)
        {
            Cart cart = GetOrCreateCart();

            foreach (CartEntry entry in cart.CartEntries)
            {
                if(entry.ProductID == item.ProductId)
                {
                    cart.CartEntries.Remove(entry);
                    _cartRepository.Update(cart);
                    return;
                }
            }
        }

        public void EmptyCart()
        {
            Cart cart = GetOrCreateCart();

            cart.CartEntries.Clear();
            _cartRepository.Update(cart);
        }

        public bool IsEmpty()
        {
            Cart cart = GetOrCreateCart();

            return cart.CartEntries.Count == 0 ? true : false;
        }


        #region Cookies Methods

        private int GetCartIdFromCookie()
        {
            string cartIdCookie = _cookieService.GetCookie(Constants.CookieKeyCartId);
            int.TryParse(cartIdCookie, out int cartId);
            return cartId;
        }

        private void SaveCartIdCookie(int cartId)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(30);
            options.HttpOnly = true;

            _cookieService.SaveCookie(Constants.CookieKeyCartId, cartId.ToString(), options);
        }

        #endregion
    }
}
