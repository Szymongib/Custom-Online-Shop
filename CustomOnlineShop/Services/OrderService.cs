using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using CustomOnlineShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomOnlineShop.Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepostory;
        ICartService _cartService;
        IProductRepository _productRepository;
        IShippingInfoRepository _shippingInfoRepository;

        #region Constructor

        public OrderService(
            IOrderRepository orderRepository,
            ICartService cartService,
            IProductRepository productRepository,
            IShippingInfoRepository shippingInfoRepository)
        {
            _orderRepostory = orderRepository;
            _cartService = cartService;
            _productRepository = productRepository;
            _shippingInfoRepository = shippingInfoRepository;
        }

        #endregion


        public Order CreateOrder(int shippingInfoId, string userId, string note)
        {
            Order order = new Order();
            
            Cart cart = _cartService.GetOrCreateCart();

            foreach (CartEntry entry in cart.CartEntries)
            {
                OrderEntry orderEntry = new OrderEntry();
                orderEntry.Product = _productRepository.GetById(entry.ProductID);
                orderEntry.Quantity = entry.Quantity;
                orderEntry.UnitPrice = orderEntry.Product.Price;
                orderEntry.ProductName = orderEntry.Product.Name;

                order.OrderEntries.Add(orderEntry);
            }

            ShippingInfo shippingInfo = _shippingInfoRepository.GetById(shippingInfoId);
            order.ShippingInfo = shippingInfo;

            order.UserId = userId;
            order.Note = note;

            order.Date = DateTime.Now;

             return _orderRepostory.Add(order);
        }

        public void ConfirmOrder(int orderId)
        {
            Order order = _orderRepostory.GetById(orderId);
            _orderRepostory.Confirm(order);
            _orderRepostory.Update(order);
        }

        public Order GetOrder(int orderId, int shippingInfoId)
        {
            Order order = _orderRepostory.GetById(orderId);
            order.OrderEntries = _orderRepostory.ListEntriesByOrderId(orderId).ToList();
            order.ShippingInfo = _shippingInfoRepository.GetById(shippingInfoId);

            return order;
        }
    }
}
