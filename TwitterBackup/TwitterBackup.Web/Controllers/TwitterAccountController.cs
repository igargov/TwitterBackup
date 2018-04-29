using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TwitterBackup.Services;
using TwitterBackup.TwitterApiClient.Contracts;

namespace TwitterBackup.API.Controllers
{
    //[Authorize]
    //[Route("[controller]/[account]")]
    public class TwitterAccountController : Controller
    {
        private readonly ITwitterApiService twitterApiService;
        private readonly ITwitterAccountService twitterAccountService;
        private readonly IMemoryCache memoryCache;

        public TwitterAccountController(ITwitterApiService twitterApiService, ITwitterAccountService twitterAccountService, IMemoryCache memoryCache)
        {
            this.twitterApiService = twitterApiService;
            this.twitterAccountService = twitterAccountService;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult FindAccount()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTwitterAccount([FromQuery] string screenName)
        {
            var twitterAccountResult = await this.memoryCache.GetOrCreateAsync(screenName, async (entry) =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(60));

                return await this.twitterApiService.RetrieveTwitterAccountAsync(screenName);
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostTwitterAccount([FromQuery] string screenName)
        {
            var twitterAccountResult = await this.memoryCache.GetOrCreateAsync(screenName, async (entry) =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(60));

                return await this.twitterApiService.RetrieveTwitterAccountAsync(screenName);
            });

            var result = this.twitterAccountService.Create(twitterAccountResult);

            return this.StatusCode(200);
        }
    }
}