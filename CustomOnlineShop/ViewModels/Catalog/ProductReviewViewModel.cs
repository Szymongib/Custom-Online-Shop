using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class ProductReviewViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string AuthorUserName { get; set; }
        public DateTime PostDate { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1,5,ErrorMessage = "Rating must be between 1-5")]
        public int Rating { get; set; }


    }
}
