using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public int ShippingInfoId { get; set; }
        public DateTime Date { get; set; }
        public bool Confirmed { get; set; } = false;
        public bool Paid { get; set; } = false;
        public bool Shipped { get; set; } = false;
        public bool Canceled { get; set; } = false;
        public string Note { get; set; }

        public ShippingInfo ShippingInfo { get; set; }
        public List<OrderEntry> OrderEntries { get; set; } = new List<OrderEntry>();

    }
}
