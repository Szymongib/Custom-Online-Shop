using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class CartIndexViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get
            {
                _totalPrice = 0;

                foreach (CartItemViewModel item in CartItems)
                {
                    _totalPrice += item.ProductPrice * item.Quantity;
                }

                return _totalPrice;
            }
        }

    }
}
