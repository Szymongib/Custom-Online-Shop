using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Data
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> ListByCategoryId(int categoryId);
    }
}
