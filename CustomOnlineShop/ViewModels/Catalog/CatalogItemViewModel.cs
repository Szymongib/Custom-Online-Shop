using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class CatalogItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ImagePath { get; set; }
        public double AverageRating { get; set; }
        public int ReviewsCount { get; set; }
    }
}
