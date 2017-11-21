using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces.Data
{
    public interface IShippingInfoRepository : IRepository<ShippingInfo>
    {
        IEnumerable<ShippingInfo> ListByUserId(string id);
        void DeleteAllByUserId(string id);
    }
}
