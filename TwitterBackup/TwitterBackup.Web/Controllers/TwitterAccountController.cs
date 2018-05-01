using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TwitterBackup.Providers;
using TwitterBackup.Services;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.Contracts;

namespace TwitterBackup.API.Controllers
{
    [Authorize]
    public class TwitterAccountController : Controller
    {
        private readonly ITwitterApiService twitterApiService;
        private readonly ITwitterAccountService twitterAccountService;
        private readonly IMappingProvider mapping;
        private readonly IMemoryCache memoryCache;

        public TwitterAccountController(ITwitterApiService twitterApiService, ITwitterAccountService twitterAccountService, 
                                        IMemoryCache memoryCache, IMappingProvider mapping)
        {
            this.twitterApiService = twitterApiService;
            this.twitterAccountService = twitterAccountService;
            this.memoryCache = memoryCache;
            this.mapping = mapping;
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

            if (twitterAccountResult == null)
            {
                
            }

            var viewModel = this.mapping.MapTo<TwitterAccountViewModel>(twitterAccountResult);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostTwitterAccount([FromBody] string screenName)
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