using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.TwitterApiClient.Contracts;
using TwitterBackup.TwitterApiClient.Models;

namespace TwitterBackup.TwitterApiClient
{
    public class TwitterApiService : ITwitterApiService
    {
        private IRestClientFactory restClientFactory;
        private IRestRequestFactory restRequestFactory;
        private TwitterAccessTokenProvider accessTokenProvider;

        public TwitterApiService(
            IRestClientFactory restClientFactory, 
            IRestRequestFactory restRequestFactory,
            TwitterAccessTokenProvider accessTokenProvider)
        {
            this.restClientFactory = restClientFactory;
            this.restRequestFactory = restRequestFactory;
            this.accessTokenProvider = accessTokenProvider;
        }

        public async Task<string> RetrieveAccountProfileImage(string url)
        {
            var client = this.restClientFactory.Create(url);
            var request = this.restRequestFactory.Create("", Method.GET);

            var result = await client.ExecuteTaskAsync<string>(request);

            string picBase64 = Convert.ToBase64String(result.RawBytes);

            return picBase64;
        }

        public async Task<TwitterAccountDTO> RetrieveTwitterAccountAsync(string screenName)
        {
            var parameters = new List<Parameter>()
            {
                {
                    new Parameter()
                    {
                        Name = "screen_name",
                        Value = screenName,
                        Type = ParameterType.QueryString
                    }
                },
            };

            var twitterAccount = 
                await this.ExecuteRequestCommonAsync<TwitterAccountDTO>(TwitterApiParams.UsersShowEndpoint, parameters);

            return twitterAccount;
        }

        public Task<TwitterStatusDTO> RetrieveTwitterStatusAsync(string statusId)
        {
            var parameters = new List<Parameter>()
            {
                {
                    new Parameter()
                    {
                        Name = "id",
                        Value = statusId,
                        Type = ParameterType.QueryString
                    }
                },
            };

            var twitterStatus = this.ExecuteRequestCommonAsync<TwitterStatusDTO>(TwitterApiParams.StatusesShowEndpoint, parameters);

            return twitterStatus;
        }

        public async Task<TwitterStatusesDTO> RetrieveTwitterAccountStatusesAsync(string screenName)
        {
            var parameters = new List<Parameter>()
            {
                {
                    new Parameter()
                    {
                        Name = "screen_name",
                        Value = screenName,
                        Type = ParameterType.QueryString
                    }
                },
            };

            var twitterAccountStatuses = 
                await this.ExecuteRequestCommonAsync<TwitterStatusesDTO>(TwitterApiParams.StatusesUserTimelineEndpoint, parameters);

            return twitterAccountStatuses;
        }

        public async Task<TwitterStatusesDTO> RetrieveTwitterAccountStatusesAsync(string screenName, int count)
        {
            var parameters = new List<Parameter>()
            {
                {
                    new Parameter()
                    {
                        Name = "screen_name",
                        Value = screenName,
                        Type = ParameterType.QueryString
                    }
                },
                {
                    new Parameter()
                    {
                        Name = "count",
                        Value = count,
                        Type = ParameterType.QueryString
                    }
                }
            };

            var twitterAccountStatuses = 
                await this.ExecuteRequestCommonAsync<TwitterStatusesDTO>(TwitterApiParams.StatusesUserTimelineEndpoint, parameters);

            return twitterAccountStatuses;
        }

        private async Task<T> ExecuteRequestCommonAsync<T>(string resource, IEnumerable<Parameter> parameters)
        {
            var restClient = this.restClientFactory.Create(TwitterApiParams.BaseUrl);
            var restRequest = this.restRequestFactory.Create(resource, Method.GET);

            var authResult = await this.accessTokenProvider.GetAccessTokenAsync();

            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddHeader("Authorization", "Bearer " + authResult.AccessToken);

            foreach (var param in parameters)
            {
                restRequest.AddParameter(param);
            }

            var result = await restClient.ExecuteTaskAsync<T>(restRequest);

            return result.Data;
        }
    }
}