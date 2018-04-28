using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Services;
using Newtonsoft.Json;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.Contracts;

namespace TwitterBackup.API.Controllers
{
    //[Authorize]
    //[Route("[controller]/[account]")]
    public class TwitterAccountController : Controller
    {
        private readonly ITwitterApiService twitterApiService;
        private readonly ITwitterAccountService twitterAccountService;

        public TwitterAccountController(ITwitterApiService twitterApiService, ITwitterAccountService twitterAccountService)
        {
            this.twitterApiService = twitterApiService;
            this.twitterAccountService = twitterAccountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTwitter(string screenName)
        {
            var twitterResult = await this.twitterApiService.RetrieveTwitterAccountAsync(screenName);

            TwitterAccountViewModel twitterAccountViewModel = null;

            //if (!twitterResult.Contains("User not found"))
            //{
            //    var jsonSettings = new JsonSerializerSettings();
            //    jsonSettings.DateFormatString = "ddd MMM dd HH:mm:ss +ffff yyyy";
            //    twitterAccountViewModel = JsonConvert.DeserializeObject<TwitterAccountViewModel>(twitterResult, jsonSettings);
            //    twitterAccountViewModel.CreatedAt = DateTime.Now;
            //    int saveResult = this.twitterAccountService.SaveAccount(twitterAccountViewModel);
            //}

            return View(twitterAccountViewModel);
        }
    }
}