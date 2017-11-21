using ApplicationInfrastructure.Identity;
using CustomOnlineShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewComponents
{
    public class ChangePassword : ViewComponent
    {
        #region Constructor

        public ChangePassword()
        {
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            return View(new ChangePasswordViewModel());
        }


    }
}
