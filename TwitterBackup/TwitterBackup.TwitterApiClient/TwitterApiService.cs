using RestSharp;
using System.Threading.Tasks;
using TwitterBackup.TwitterApiClient.Contracts;

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

        public async Task<string> RetrieveTwitterAccountAsync(string screenName)
        {
            var twitterAccount = await this.ExecuteRequestCommonAsync<string>(screenName, TwitterApiParams.UsersShowEndpoint);

            return twitterAccount;
        }

        public async Task<string> RetrieveTwitterAccountStatusesAsync(string screenName)
        {
            var twitterAccountStatuses = await this.ExecuteRequestCommonAsync<string>(screenName, TwitterApiParams.StatusesUserTimelineEndpoint);

            return twitterAccountStatuses;
        }

        private async Task<T> ExecuteRequestCommonAsync<T>(string screenName, string resource)
        {
            var restClient = this.restClientFactory.Create(TwitterApiParams.BaseUrl);
            var restRequest = this.restRequestFactory.Create(resource, Method.GET);

            var authResult = await this.accessTokenProvider.GetAccessTokenAsync();

            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddHeader("Authorization", "Bearer " + authResult.AccessToken); 
            restRequest.AddQueryParameter("screen_name", screenName);

            var result = await restClient.ExecuteTaskAsync<T>(restRequest);

            return result.Data;
        }
    }
}