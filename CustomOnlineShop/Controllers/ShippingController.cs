using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using ApplicationInfrastructure.Identity;
using CustomOnlineShop.Helpers;
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
    [Authorize]
    public class ShippingController : Controller
    {
        UserManager<AppUser> _userManager;
        IShippingInfoRepository _shippingInfoRepository;

        #region Constructor

        public ShippingController(UserManager<AppUser> userManager, IShippingInfoRepository shippingInfoRepository)
        {
            _userManager = userManager;
            _shippingInfoRepository = shippingInfoRepository;
        }

        #endregion


        [HttpGet]
        public ViewResult CreateInfo(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new ShippingInfoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateInfo(ShippingInfoViewModel model ,string returnUrl)
        {
            // Get current User
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                if(model.Id <= 0)
                {
                    ShippingInfo info = new ShippingInfo
                    {
                        UserId = user.Id,
                        Country = model.Country,
                        City = model.City,
                        ZipCode = model.ZipCode,
                        Address = model.Address
                    };

                    _shippingInfoRepository.Add(info);
                }
                else
                {
                    ShippingInfo info = _shippingInfoRepository.GetById(model.Id);

                    info.Country = model.Country;
                    info.City = model.City;
                    info.ZipCode = model.ZipCode;
                    info.Address = model.Address;

                    _shippingInfoRepository.Update(info);

                    return RedirectToAction("MyAccount", "Account");
                }

                return Redirect(returnUrl);
            }

            ViewBag.returnUrl = returnUrl;

            return View(model);
        }

        public IActionResult Edit(int infoId)
        {
            ShippingInfoViewModel model = _shippingInfoRepository.GetById(infoId).MapToViewModel();

            return View("CreateInfo", model);
        }

        public IActionResult Delete(int Id)
        {
            _shippingInfoRepository.Delete(_shippingInfoRepository.GetById(Id));

            return RedirectToAction("MyAccount", "Account");
        }

    }
}
