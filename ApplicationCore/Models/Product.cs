using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ImgPath { get; set; }
        public int CategoryId { get; set; }

        public List<ProductReview> ProductReviews { get; set; }
        public ProductCategory Category { get; set; }

    }
}
