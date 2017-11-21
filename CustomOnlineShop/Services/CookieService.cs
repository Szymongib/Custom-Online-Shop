using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CustomOnlineShop.Services.Interfaces;

namespace CustomOnlineShop.Services
{
    public class CookieService : ICookieService
    {
        IHttpContextAccessor _httpContextAccessor;

        #region Constructor

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion
        

        public string GetCookie(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }

        public void SaveCookie(string key, string value)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value);
        }

        public void SaveCookie(string key, string value, CookieOptions options)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, options);
        }
    }
}
