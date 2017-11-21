using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class CheckoutIndexViewModel
    {
        public List<ShippingInfoViewModel> ShippingInfoOptions { get; set; }
        public OrderDataViewModel OrderData { get; set; }
    }
}
