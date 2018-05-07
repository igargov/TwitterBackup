using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class TwitterAccountStatusController : Controller
    {
        private ITwitterAccountService twitterAccountService;
        private ITwitterStatusService twitterStatusService;
        private UserManager<User> userManager;

        public TwitterAccountStatusController(ITwitterAccountService twitterAccountService, ITwitterStatusService twitterStatusService, UserManager<User> userManager)
        {
            this.twitterAccountService = twitterAccountService;
            this.twitterStatusService = twitterStatusService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Details(int accountId)
        {
            var userId = int.Parse(this.userManager.GetUserId(this.User));

            var twitterAccount = this.twitterAccountService.GetById(accountId, userId);
            var twitterAccountStatuses = this.twitterStatusService.GetAll(userId);

            var detailsModel = new TwitterAccountStatusDetailsViewModel();
            detailsModel.TwitterAccount = twitterAccount;
            detailsModel.TwitterStatuses = twitterAccountStatuses;

            return View(detailsModel);
        }
    }
}