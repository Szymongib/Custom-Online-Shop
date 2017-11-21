using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomOnlineShop.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(int shippingInfoId, string userId, string note);
        void ConfirmOrder(int orderId);
        Order GetOrder(int orderId, int shippingInfoId);
    }
}
