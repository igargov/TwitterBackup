using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Services;
using TwitterBackup.Services.Contracts;

namespace TwitterBackup.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class AdminController : Controller
    {
        private readonly TwitterBackup.Services.IUserService userService;
        private readonly UserManager<User> userManager;

        public AdminController(TwitterBackup.Services.IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowAllUsers()
        {
            var userViewModels = this.userService.GetUsers();

            return View(userViewModels);
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
    }
}