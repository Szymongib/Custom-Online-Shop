using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using ApplicationInfrastructure.Identity;
using CustomOnlineShop.Helpers;
using CustomOnlineShop.Services.Interfaces;
using CustomOnlineShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        IPasswordHasher<AppUser> _passwordHasher;
        IShippingInfoRepository _shippingInfoRepository;
        IOrderService _orderService;
        IOrderRepository _orderRepository;
        IPasswordValidator<AppUser> _passwordValidator;

        #region Constructor

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, 
            IPasswordHasher<AppUser> passwordHasher,
            IShippingInfoRepository shippingInfoRepository,
            IOrderService orderService,
            IOrderRepository orderRepository,
            IPasswordValidator<AppUser> passwordValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _shippingInfoRepository = shippingInfoRepository;
            _orderService = orderService;
            _orderRepository = orderRepository;
            _passwordValidator = passwordValidator;
        }

        #endregion


        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // Get current loged in user
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.returnUrl = returnUrl ?? "/";

            if(user != null)
            {
                return RedirectToAction("MyAccount", "Account");
            }
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // Get current loged in user
                AppUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                    else
                    {
                        ModelState.AddModelError("InvalidUserOrPassword", "Invalid email adress or password!");
                    }
                }
                else
                {
                    ModelState.AddModelError("InvalidUserOrPassword", "Invalid email adress or password!");
                }
            }

            ViewBag.returnUrl = returnUrl;

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Register(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber
                };
                
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Redirect(returnUrl);
                }
                else
                {
                    AddErrorsFromResult(result);
                }                
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        

        [Authorize]
        public async Task<IActionResult> MyAccount()
        {
            // Get current loged in user
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            List<ShippingInfo> shippingOptions = _shippingInfoRepository.ListByUserId(user.Id).ToList();

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(CreateMyAccountVM(user, shippingOptions));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditUser()
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            return View(new EditUserViewModel { FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("MyAccount");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(model);
        }              

        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            }

            return RedirectToAction("MyAccount");
        }

        // Remote validation action
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> VerifyOldPassword(string oldPassword)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);

            if (result != PasswordVerificationResult.Success)
            {
                return Json(data: $"Old password is invalid.");
            }

            return Json(data: true);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> VerifyNewPassword(string newPassword)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            IdentityResult result = await _passwordValidator.ValidateAsync(_userManager, user, newPassword);

            if (!result.Succeeded)
            {
                string errors = "";
                foreach (IdentityError error in result.Errors)
                {
                    errors += error.Description+"\r\n";
                }
                return Json(data: errors);
            }

            return Json(data: true);
        }

        [Authorize]
        public async Task<IActionResult> DisplayOrders()
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            List<Order> orders = _orderRepository.ListAllByUserId(user.Id).ToList();
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();

            foreach (Order ord in orders)
            {
                ord.OrderEntries = _orderRepository.ListEntriesByOrderId(ord.Id).ToList();
                ord.ShippingInfo = _shippingInfoRepository.GetById(ord.ShippingInfoId);
                orderViewModels.Add(ord.MapToViewModel());
            }

            return View(new DisplayOrdersViewModel { Orders = orderViewModels });
        }

        public ViewResult AccessDenied()
        {
            return View();
        }






        private MyAccountViewModel CreateMyAccountVM(AppUser user, List<ShippingInfo> shippingOptions)
        {
            List<ShippingInfoViewModel> list = new List<ShippingInfoViewModel>();

            foreach (ShippingInfo info in shippingOptions)
            {
                list.Add(info.MapToViewModel());
            }

            return new MyAccountViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserShippingOptions = list
            };
        }


        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }


    }
}
