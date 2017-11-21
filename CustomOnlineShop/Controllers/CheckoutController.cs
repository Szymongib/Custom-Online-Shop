using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using ApplicationInfrastructure.Identity;
using CustomOnlineShop.Helpers;
using CustomOnlineShop.Services.Interfaces;
using CustomOnlineShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.Controllers
{

    [Authorize]
    public class CheckoutController : Controller
    {
        SignInManager<AppUser> _signInManager;
        UserManager<AppUser> _userManager;
        IShippingInfoRepository _shippingInfoRepository;
        ICartService _cartService;
        IOrderService _orderService;

        #region Constructor

        public CheckoutController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, 
            IShippingInfoRepository shippingInfoRepository,
            ICartService cartService, 
            IOrderService orderService )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _shippingInfoRepository = shippingInfoRepository;
            _cartService = cartService;
            _orderService = orderService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            // Get current loged in user
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            List<ShippingInfo> shippingInfoList = _shippingInfoRepository.ListByUserId(user.Id)?.ToList() ?? new List<ShippingInfo>();

            if(shippingInfoList.Count <= 0)
            {
                return RedirectToAction("CreateInfo", "Shipping", new { returnUrl = "/Checkout/" });
            }
            
            return View(CreateCheckoutIndexVM(shippingInfoList, shippingInfoList[0].Id));
        }


        public async Task<IActionResult> CreateOrder(OrderDataViewModel model)
        {
            // Get curreny user
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                int cartId = _cartService.GetCartId();

                Order order = _orderService.CreateOrder(model.SelectedShippingInfoId, user.Id, model.Note);

                OrderViewModel orderModel = new OrderViewModel
                {
                    OrderId = order.Id,
                    Date = order.Date,
                    ShippingInfo = _shippingInfoRepository.GetById(model.SelectedShippingInfoId).MapToViewModel(),
                    OrderEntries = order.OrderEntries.Select(x => x.MapToViewModel()).ToList(),

                    PaymentMethod = model.SelectedPaymentMethod,
                };
                
                return RedirectToAction("OrderSummary", "Checkout", new { orderId = order.Id, selectedPayment = model.SelectedPaymentMethod, shippingInfoId = model.SelectedShippingInfoId });
            }

            return RedirectToAction("Index");
        }

        public IActionResult OrderSummary(int orderId, int shippingInfoId, string selectedPayment)
        {
            OrderViewModel orderModel = _orderService.GetOrder(orderId, shippingInfoId).MapToViewModel();
            orderModel.PaymentMethod = selectedPayment;

            return View("ConfirmOrder", orderModel);
        }

        public IActionResult ConfirmOrder(int orderId)
        {
            _orderService.ConfirmOrder(orderId);
            _cartService.EmptyCart();
            
            return RedirectToAction("OrderCompleted");
        }
        
        public ViewResult OrderCompleted()
        {
            return View();
        }
        


        
        private CheckoutIndexViewModel CreateCheckoutIndexVM(List<ShippingInfo> shippingInfoList, int selectedShippingInfoId)
        {
            List<ShippingInfoViewModel> list = shippingInfoList.Select(x => x.MapToViewModel()).ToList();            

            return new CheckoutIndexViewModel
            {
                ShippingInfoOptions = list,
                OrderData = new OrderDataViewModel { SelectedShippingInfoId = selectedShippingInfoId }
            };
        }
        
    }
}
