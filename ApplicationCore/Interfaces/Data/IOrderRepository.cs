using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Data
{
    public interface IOrderRepository : IRepository<Order>
    {

        IEnumerable<Order> ListAllByUserId(string id);

        IEnumerable<OrderEntry> ListEntriesByOrderId(int orderId);

        void Confirm(Order order);
    }
}
