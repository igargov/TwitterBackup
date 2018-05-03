using System;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Services;

namespace TwitterBackup.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;

        public AdminController(IUserService userService)
        {
            this.userService = userService;
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
        public IActionResult PromoteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("message", nameof(userId));
            }

            try
            {
                //Service Promote User

                return this.Json(new { isPromoted = true });
            }
            catch (Exception)
            {
                return this.Json(new { isPromoted = false });
            }
        }
    }
}