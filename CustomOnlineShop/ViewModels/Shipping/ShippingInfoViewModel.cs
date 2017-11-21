using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class ShippingInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Invalid zip code")]
        public string ZipCode { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
