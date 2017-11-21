using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class CartEntry : BaseEntity
    {
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }       

    }
}
