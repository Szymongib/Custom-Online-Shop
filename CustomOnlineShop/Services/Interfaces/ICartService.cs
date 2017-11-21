using ApplicationCore.Models;
using CustomOnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.Services.Interfaces
{
    public interface ICartService
    {
        int GetCartId();
        Cart GetOrCreateCart();
        void AddToCart(CatalogItemViewModel model);
        void RemoveFromCart(CartItemViewModel item);
        void UpdateCartEntry(int productId, int quantity);
        void EmptyCart();
        bool IsEmpty();
    }
}
