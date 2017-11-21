using CustomOnlineShop.Services.Interfaces;
using CustomOnlineShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewComponents
{
    public class CartComponent : ViewComponent
    {
        ICartService _cartService;

        #region Consturctor

        public CartComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        #endregion


        public IViewComponentResult Invoke()
        {
            CartComponentViewModel vm = new CartComponentViewModel();

            vm.ItemsCount = _cartService.GetOrCreateCart().CartEntries.Count;

            return View(vm);
        }


    }
}
