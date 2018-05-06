using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TwitterBackup.TwitterApiClient.Contracts;

namespace TwitterBackup.Web.Controllers
{
    public class TwitterStatusController : Controller
    {
        private ITwitterApiService twitterApiService;

        public TwitterStatusController(ITwitterApiService twitterApiService)
        {
            this.twitterApiService = twitterApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveStatuses(string screenName)
        {
            var tweets = await this.twitterApiService.RetrieveTwitterAccountStatusesAsync(screenName);

            return this.Ok();
        }
    }
}