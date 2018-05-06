using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Services.Contracts;
using TwitterBackup.TwitterApiClient.Contracts;

namespace TwitterBackup.Web.Controllers
{
    public class TwitterStatusController : Controller
    {
        private ITwitterApiService twitterApiService;
        private ITwitterStatusService twitterStatusService;

        public TwitterStatusController(ITwitterApiService twitterApiService, ITwitterStatusService twitterStatusService)
        {
            this.twitterApiService = twitterApiService;
            this.twitterStatusService = twitterStatusService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListAllStatuses(int accountId)
        {
            var statuses = this.twitterStatusService.GetAll(accountId);

            return this.Ok();
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveStatuses(string screenName)
        {
            var tweets = await this.twitterApiService.RetrieveTwitterAccountStatusesAsync(screenName, 1);

            try
            {
                var result = this.twitterStatusService.Create(tweets.First());
            }
            catch (Exception ex)
            {
            }

            return this.Ok();
        }
    }
}