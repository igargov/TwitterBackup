using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class AdminController : Controller
    {
        private readonly IMappingProvider mappingProvider;
        private readonly ITwitterAccountService twitterAccountService;
        private readonly UserManager<User> userManager;

        public AdminController(IMappingProvider mappingProvider, ITwitterAccountService twitterAccountService, UserManager<User> userManager)
        {
            this.mappingProvider = mappingProvider;
            this.twitterAccountService = twitterAccountService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ShowAllUsers()
        {
            var users = new List<User>();

            foreach (User user in this.userManager.Users)
            {
                var userRoles = await this.userManager.GetRolesAsync(user);

                if (userRoles.Contains("User"))
                {
                    users.Add(user);
                }
            }

            var usersAsViewModels = this.mappingProvider.ProjectTo<UserViewModel>(users.AsQueryable<User>());

            return View(usersAsViewModels);
        }

        public async Task<IActionResult> ShowAllAdmins()
        {
            var admins = new List<User>();

            foreach (User user in this.userManager.Users)
            {
                var userRoles = await this.userManager.GetRolesAsync(user);

                if (userRoles.Contains("Admin"))
                {
                    admins.Add(user);
                }
            }

            var usersAsViewModels = this.mappingProvider.ProjectTo<UserViewModel>(admins.AsQueryable<User>());

            return View(usersAsViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoteUser(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentException();
            }

            var user = await this.userManager.FindByIdAsync(Id);
            if (user == null)
            {
                throw new ArgumentException();
            }

            await this.userManager.RemoveFromRoleAsync(user, "User");
            await this.userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("ShowAllUsers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DelUser(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentException();
            }

            var user = await this.userManager.FindByIdAsync(Id);
            await this.userManager.DeleteAsync(user);
            return RedirectToAction("ShowAllUsers");
        }

        [HttpGet]
        public IActionResult SavedTwitters(string userId)
        {
            return RedirectToAction("ListAllAccounts", "TwitterAccount", new { area = "", externalUserId = userId });
        }
    }
}