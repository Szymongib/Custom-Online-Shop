using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using CustomOnlineShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.Services
{
    public class CachedCartService : CartService ,ICartService
    {
        #region Constructor

        public CachedCartService(ICartRepository cartRepository, IProductRepository productRepository) : 
            base(cartRepository,null,productRepository)
        {   }

        #endregion

        static Cart CachedCart;


        public override int GetCartId()
        {
            return CachedCart.Id;
        }

        public override Cart GetOrCreateCart()
        {
            if(CachedCart == null)
            {
                CachedCart = new Cart();
                _cartRepository.Add(CachedCart);
            }

            return CachedCart;
        }
    }
}
