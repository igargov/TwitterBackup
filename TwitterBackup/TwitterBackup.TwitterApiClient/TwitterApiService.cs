using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.TwitterApiClient.Contracts;
using TwitterBackup.TwitterApiClient.TwitterModels;

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
                await this.ExecuteRequestCommonAsync<TwitterAccountDTO>(screenName, TwitterApiParams.UsersShowEndpoint, parameters);

            return twitterAccount;
        }

        public async Task<string> RetrieveTwitterAccountStatusesAsync(string screenName)
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
                await this.ExecuteRequestCommonAsync<string>(screenName, TwitterApiParams.StatusesUserTimelineEndpoint, parameters);

            return twitterAccountStatuses;
        }

        public async Task<string> RetrieveTwitterAccountStatusesAsync(string screenName, int count)
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
                await this.ExecuteRequestCommonAsync<string>(screenName, TwitterApiParams.StatusesUserTimelineEndpoint, parameters);

            return twitterAccountStatuses;
        }

        private async Task<T> ExecuteRequestCommonAsync<T>(string screenName, string resource, IEnumerable<Parameter> parameters)
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