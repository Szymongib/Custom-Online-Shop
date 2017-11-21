using ApplicationInfrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInfrastructure.Data.Helpers
{
    public static class IdentityDbInitializer
    {
        public static async Task InitializeWithDummyData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            ApplicationIdentityDbContext _context = serviceProvider.GetRequiredService<ApplicationIdentityDbContext>();

            if (!_context.Users.Any())
            {
                UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await CreateDefaultAdminAccount(_userManager, _roleManager, configuration);

                AppUser defaultUser = new AppUser
                {
                    UserName = "DefaultUser",
                    Email = "default@email.com",
                    FirstName = "Default",
                    LastName = "User",
                    PhoneNumber = "811928938"
                };

                string password = "%secret123";

                await _userManager.CreateAsync(defaultUser, password);
            }
            
        }
        

        public static async Task CreateDefaultAdminAccount(UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager, IConfiguration configuration)
        {
            string username = configuration["AdminAccount:UserName"];
            string email = configuration["AdminAccount:Email"];
            string password = configuration["AdminAccount:Password"];
            string role = configuration["AdminAccount:Role"];
            string firstName = configuration["AdminAccount:FirstName"];
            string lastName = configuration["AdminAccount:LastName"];


            // Check if Admin role exists and if there are any users assigned to it
            IdentityRole adminRole = await _roleManager.FindByNameAsync(role);
            if (adminRole != null)
            {
                var admins = await _userManager.GetUsersInRoleAsync(role);
                if (admins != null && admins.Count != 0)
                {
                    return;
                }
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            AppUser admin = new AppUser
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            IdentityResult result = await _userManager.CreateAsync(admin, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, role);
            }

        }

    }
}
