using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Providers;
using TwitterBackup.Services.Contracts;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.Contracts;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Web.Controllers
{
    public class TwitterStatusController : Controller
    {
        private ITwitterApiService twitterApiService;
        private ITwitterStatusService twitterStatusService;
        private IMappingProvider mapper;

        public TwitterStatusController(ITwitterApiService twitterApiService, ITwitterStatusService twitterStatusService, IMappingProvider mapper)
        {
            this.twitterApiService = twitterApiService;
            this.twitterStatusService = twitterStatusService;
            this.mapper = mapper;
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
            try
            {
                var statuses = await this.twitterApiService.RetrieveTwitterAccountStatusesAsync(screenName);

                var statusesModel = this.mapper.MapTo<IEnumerable<TwitterStatusDTO>, IEnumerable<TwitterStatusViewModel>>(statuses);

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

                var statusesModel = this.mapper.MapTo<IEnumerable<TwitterStatusDTO>, IEnumerable<TwitterStatusViewModel>>(statuses);

                return PartialView("_TwitterStatusPartial", statusesModel);
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }
        }
    }
}