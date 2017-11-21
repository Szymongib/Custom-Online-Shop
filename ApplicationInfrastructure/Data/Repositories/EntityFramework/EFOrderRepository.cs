using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace ApplicationInfrastructure.Data.Repositories
{
    public class EFOrderRepository : EFBaseRepository<Order>, IOrderRepository
    {

        public EFOrderRepository(OnlineShopDbContext context): base(context)
        {  }


        public IEnumerable<Order> ListAllByUserId(string id)
        {
            return _context.Orders.Where(x => x.UserId == id).AsEnumerable();
        }

        public IEnumerable<OrderEntry> ListEntriesByOrderId(int orderId)
        {
            return _context.OrderEntries.Where(x => x.OrderID == orderId);
        }

        public void Confirm(Order order)
        {
            order.Confirmed = true;
        }


    }
}
