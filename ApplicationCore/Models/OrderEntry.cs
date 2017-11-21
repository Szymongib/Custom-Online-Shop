using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class OrderEntry : BaseEntity
    {
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
