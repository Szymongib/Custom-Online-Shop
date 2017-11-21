using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class CatalogIndexViewModel
    {
        public IEnumerable<CatalogItemViewModel> Items { get; set; }
        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public string SearchedPhrase { get; set; }
    }
}
