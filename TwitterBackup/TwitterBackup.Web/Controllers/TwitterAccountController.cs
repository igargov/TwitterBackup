﻿using System;
using System.Linq;
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

        public TwitterAccountController(ITwitterApiService twitterApiService, ITwitterAccountService twitterAccountService, IMemoryCache memoryCache, IMappingProvider mapping)
        {
            this.twitterApiService = twitterApiService;
            this.twitterAccountService = twitterAccountService;
            this.memoryCache = memoryCache;
            this.mapping = mapping;
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveAccount(string screenName)
        {
            var twitterAccountResult = await this.memoryCache.GetOrCreateAsync(screenName, async (entry) =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(60));

                return await this.twitterApiService.RetrieveTwitterAccountAsync(screenName);
            });

            ViewData.Add("SearchScreenName", screenName);

            if (twitterAccountResult.Errors != null)
            {
                var error = twitterAccountResult.Errors.First();

                var errModel = this.mapping.MapTo<TwitterErrorViewModel>(error);

                return View("TwitterError", errModel);
            }

            var viewModel = this.mapping.MapTo<TwitterAccountViewModel>(twitterAccountResult);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(string screenName)
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