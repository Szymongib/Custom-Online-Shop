using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class CreateProductViewModel
    {
        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Price (per unit)")]
        [RegularExpression("^[0-9]{0,999999999}([,.][0-9]{1,2})?", ErrorMessage = "Please enter valid price.")]
        public string Price { get; set; }
        
        [Required]
        [Display(Name = "Full Description")]
        public string FullDescription { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Image Name")]
        public string ImgPath { get; set; }

    }
}
