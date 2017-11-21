using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ApplicationInfrastructure.Data.Repositories
{
    public class EFProductCategoryRepository : EFBaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public EFProductCategoryRepository(OnlineShopDbContext context) : base(context)
        {  }

    }
}
