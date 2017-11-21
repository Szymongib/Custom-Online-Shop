using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using ApplicationInfrastructure.Data.Helpers;

namespace ApplicationInfrastructure.Data.Repositories
{
    public class EFProductRepository : EFBaseRepository<Product>, IProductRepository
    {

        public EFProductRepository(OnlineShopDbContext context) : base(context)
        {   }

        public IEnumerable<Product> ListByCategoryId(int categoryId)
        {
            return _context.Products.Where(x => x.CategoryId == categoryId);
        }
    }
}
