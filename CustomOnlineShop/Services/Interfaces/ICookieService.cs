using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.Services.Interfaces
{
    public interface ICookieService
    {
        string GetCookie(string key);

        void SaveCookie(string key, string value);

        void SaveCookie(string key, string value, CookieOptions options);

    }
}
