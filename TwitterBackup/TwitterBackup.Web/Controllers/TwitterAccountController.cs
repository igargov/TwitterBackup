using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.Contracts;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class TwitterAccountController : Controller
    {
        private readonly ITwitterApiService twitterApiService;
        private readonly ITwitterAccountService twitterAccountService;
        private readonly IMappingProvider mapping;
        private readonly IMemoryCache memoryCache;
        private readonly UserManager<User> userManager;

        public TwitterAccountController(
            ITwitterApiService twitterApiService,
            ITwitterAccountService twitterAccountService,
            IMemoryCache memoryCache,
            IMappingProvider mapping,
            UserManager<User> userManager
            )
        {
            this.twitterApiService = twitterApiService;
            this.twitterAccountService = twitterAccountService;
            this.memoryCache = memoryCache;
            this.mapping = mapping;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListAllAccounts()
        {
            int userId = int.Parse(this.userManager.GetUserId(this.User));

            var allAccounts = this.twitterAccountService.GetAll(userId);

            if (allAccounts == null)
            {
                return View("_NotFound");
            }

            return View(allAccounts);
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveAccount(string screenName)
        {
            var twitterAccountResult = await this.memoryCache.GetOrCreateAsync(screenName, async (entry) =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromMinutes(10));

                return await this.twitterApiService.RetrieveTwitterAccountAsync(screenName);
            });

            ViewData.Add("SearchScreenName", screenName);

            if (twitterAccountResult.Errors != null)
            {
                var errors = twitterAccountResult.Errors;

                var errModel = this.mapping.ProjectTo<TwitterErrorViewModel>(errors);

                return View("TwitterError", errModel);
            }

            var viewModel = this.mapping.MapTo<TwitterAccountViewModel>(twitterAccountResult);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(string screenName)
        {
            bool isAccountPresent = this.memoryCache.TryGetValue(screenName, out TwitterAccountDTO twitterAccount);

            if (!isAccountPresent)
            {
                twitterAccount = await this.twitterApiService.RetrieveTwitterAccountAsync(screenName);
            }

            string accountImage = await this.twitterApiService.RetrieveAccountProfileImage(twitterAccount.ProfileImageUrl);

            int userId = int.Parse(this.userManager.GetUserId(this.User));

            try
            {
                var result = this.twitterAccountService.Create(twitterAccount, userId, accountImage);

                return this.Ok(new { accountId = result });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { result = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteAccount(int accountId)
        {
            var userId = int.Parse(this.userManager.GetUserId(this.User));

            bool isDeleted = this.twitterAccountService.Delete(accountId, userId);

            return this.Ok(new { success = isDeleted });
        }
    }
}