using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.ViewModels
{
    public class ChangePasswordViewModel
    {
        // Remote validation
        [Required]
        [Display(Name ="Old Password")]
        [DataType(DataType.Password)]
        [Remote(action: "VerifyOldPassword", controller: "Account")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [Remote(action: "VerifyNewPassword", controller: "Account")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
