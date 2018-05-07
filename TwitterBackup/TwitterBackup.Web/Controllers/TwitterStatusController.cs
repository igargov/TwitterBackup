using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.Contracts;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class TwitterStatusController : Controller
    {
        private ITwitterApiService twitterApiService;
        private ITwitterStatusService twitterStatusService;
        private IMappingProvider mappingProvider;
        private UserManager<User> userManager;

        public TwitterStatusController(
            ITwitterApiService twitterApiService,
            ITwitterStatusService twitterStatusService,
            IMappingProvider mappingProvider,
            UserManager<User> userManager)
        {
            this.twitterApiService = twitterApiService;
            this.twitterStatusService = twitterStatusService;
            this.mappingProvider = mappingProvider;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListAllStatuses()
        {
            var userId = int.Parse(this.userManager.GetUserId(this.User));

            var statuses = this.twitterStatusService.GetAll(userId);

            return View(statuses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus(string statusId)
        {
            try
            {
                int userId = int.Parse(this.userManager.GetUserId(this.User));

                var status = await this.twitterApiService.RetrieveTwitterStatusAsync(statusId);

                var result = this.twitterStatusService.Create(status, userId);

                return this.Ok(new { Id = result });
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveStatuses(string screenName)
        {
            try
            {
                var statuses = await this.twitterApiService.RetrieveTwitterAccountStatusesAsync(screenName);

                var statusesModel = this.mappingProvider.MapTo<IEnumerable<TwitterStatusDTO>, IEnumerable<TwitterStatusViewModel>>(statuses);

                return PartialView("_TwitterStatusPartial", statusesModel);
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveStatusesByCount(string screenName, int count)
        {
            try
            {
                var statuses = await this.twitterApiService.RetrieveTwitterAccountStatusesAsync(screenName, count);

                var statusesModel = this.mappingProvider.MapTo<IEnumerable<TwitterStatusDTO>, IEnumerable<TwitterStatusViewModel>>(statuses);

                return PartialView("_TwitterStatusPartial", statusesModel);
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DeleteStatus(int statusId)
        {
            var userId = int.Parse(this.userManager.GetUserId(this.User));

            bool isDeleted = this.twitterStatusService.Delete(statusId, userId);

            return this.Ok(new { success = isDeleted });
        }
    }
}