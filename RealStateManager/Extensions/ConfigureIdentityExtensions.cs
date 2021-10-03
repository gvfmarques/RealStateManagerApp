using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealStateManager.Extensions
{
    public static class ConfigureIdentityExtensions
    {
        public static void ConfigureNameUser(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options => {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwyzABCDEFGAHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
        }

        public static void ConfigureUserPassword(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 0;
            });
        }
    }
}
