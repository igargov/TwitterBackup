using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.API.Models;
using TwitterBackup.Providers;
using TwitterBackup.Providers.Contracts;
using TwitterBackup.Services;
using Newtonsoft.Json;
using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.API.Controllers
{
    [Authorize]
    //[Route("[controller]/[account]")]
    public class TwitterAccountController : Controller
    {
        private readonly ITwitterFacadeProvider twitterFacadeProvider;
        private readonly TwitterOAuthProvider twitterOAuthProvider;
        private readonly ITwitterAccountService twitterAccountService;

        public TwitterAccountController(ITwitterFacadeProvider twitterFacadeProvider, TwitterOAuthProvider twitterOAuthProvider, ITwitterAccountService twitterAccountService)
        {
            this.twitterFacadeProvider = twitterFacadeProvider;
            this.twitterOAuthProvider = twitterOAuthProvider;
            this.twitterAccountService = twitterAccountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTwitter(string screenName)
        {
            var accessToken = await this.twitterOAuthProvider.GetAccessTokenAsync();
            var twitterResult = await this.twitterFacadeProvider.RetrieveTwitterAccountAsync(screenName, accessToken);

            TwitterAccountViewModel twitterAccountViewModel = null;

            if (!twitterResult.Contains("User not found"))
            {
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.DateFormatString = "ddd MMM dd HH:mm:ss +ffff yyyy";
                twitterAccountViewModel = JsonConvert.DeserializeObject<TwitterAccountViewModel>(twitterResult, jsonSettings);
                twitterAccountViewModel.CreatedAt = DateTime.Now;
                int saveResult = this.twitterAccountService.SaveAccount(twitterAccountViewModel);
            }

            return View(twitterAccountViewModel);
        }
    }
}