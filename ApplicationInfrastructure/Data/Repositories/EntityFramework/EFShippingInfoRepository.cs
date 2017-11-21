using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using System.Linq;
using ApplicationCore.Interfaces.Data;

namespace ApplicationInfrastructure.Data.Repositories
{
    public class EFShippingInfoRepository : EFBaseRepository<ShippingInfo>, IShippingInfoRepository
    {

        public EFShippingInfoRepository(OnlineShopDbContext context) : base(context)
        {   }

        public void DeleteAllByUserId(string id)
        {
            IEnumerable<ShippingInfo> toDelete = _context.ShippingInfo.Where(x => x.UserId == id);
            _context.ShippingInfo.RemoveRange(toDelete);
            _context.SaveChanges();
        }

        public IEnumerable<ShippingInfo> ListByUserId(string id)
        {
            return _context.ShippingInfo.Where(x => x.UserId == id);
        }
    }
}
