using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TwitterBackup.Data;
using TwitterBackup.Data.Models.Identity;

namespace TwitterBackup.Web.Seed
{
    public class SeedDatabase
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, TwitterBackupDbContext context)
        {
            var roleManager = serviceProvider.GetService<RoleManager<Role>>();
            var userManager = serviceProvider.GetService<UserManager<User>>();

            string[] roleNames = { "Admin", "User" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role(roleName));
                }
            }

            var adminSettings = Startup.Configuration.GetSection("AdminData");

            string userName = adminSettings.GetValue<string>("UserName");
            string userEmail = adminSettings.GetValue<string>("UserEmail");
            string userPassword = adminSettings.GetValue<string>("UserPassword");

            var admin = new User()
            {
                UserName = userName,
                Email = userEmail
            };

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(admin, userPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
