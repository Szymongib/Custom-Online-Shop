using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ProductReview : BaseEntity
    {
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostDate { get; set; }
        public int ProductID { get; set; }
        public string AuthorID { get; set; }
        public string AuthorUserName { get; set; }

        public Product Product { get; set; }
    }
}
