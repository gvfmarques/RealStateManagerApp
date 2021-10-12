using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Extensions
{
    public static class CookiesConfigurationExtension
    {
        public static void ConfigureCookies(this IServiceCollection service)
        {
            service.ConfigureApplicationCookie(options => {
                options.Cookie.Name = "IdentityCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = "/User/AccessDenied";
            });
        }
    }
}
