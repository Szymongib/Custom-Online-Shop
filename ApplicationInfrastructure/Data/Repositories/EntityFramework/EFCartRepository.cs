using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationInfrastructure.Data;
using Microsoft.AspNetCore.Http;
using ApplicationCore.Models;
using ApplicationCore.Interfaces.Data;

namespace ApplicationInfrastructure.Data.Repositories
{
    public class EFCartRepository : EFBaseRepository<Cart>, ICartRepository
    {

        public EFCartRepository(OnlineShopDbContext context) : base(context)
        {  }

        public IEnumerable<CartEntry> ListEntriesByCartId(int id)
        {
            return _context.CartEntries.Where(x => x.CartID == id);
        }

        
    }
}
