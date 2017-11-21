using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public List<OrderEntryViewModel> OrderEntries { get; set; } = new List<OrderEntryViewModel>();
        public DateTime Date { get; set; }
        public ShippingInfoViewModel ShippingInfo { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal tp = 0;
                foreach (OrderEntryViewModel entry in OrderEntries)
                {
                    tp += entry.EntryPrice;
                }
                return tp;
            }
        }
             

    }
}
