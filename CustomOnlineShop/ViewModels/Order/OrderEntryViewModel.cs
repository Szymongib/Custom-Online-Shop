using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class OrderEntryViewModel
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal EntryPrice
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
                
    }
}
