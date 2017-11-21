using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class OrderDataViewModel
    {
        [Required]
        public int SelectedShippingInfoId { get; set; }

        [Required]
        public string SelectedPaymentMethod { get; set; }

        [StringLength(200,ErrorMessage ="Note can not be longer than 200 characters.")]
        public string Note { get; set; }
    }
}
