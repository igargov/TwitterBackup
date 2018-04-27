using RestSharp;
using System.Threading.Tasks;
using TwitterBackup.Providers.Contracts;

namespace TwitterBackup.Providers.TwitterProviders
{
    public class TwitterFacadeProvider : ITwitterFacadeProvider
    {
        private IRestClientFactory restClient;
        private IRestRequestFactory restRequest;

        public TwitterFacadeProvider(IRestClientFactory restClientFactory, IRestRequestFactory restRequestFactory)
        {
            this.restClient = restClientFactory;
            this.restRequest = restRequestFactory;
        }

        public async Task<string> RetrieveTwitterAccountAsync(string screenName, string token)
        {
            var client = this.restClient.Create(TwitterFacadeParams.BaseUrl);
            var request = this.restRequest.Create(TwitterFacadeParams.UsersShowEndpoint, Method.GET);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddQueryParameter("screen_name", screenName);

            var result = await client.ExecuteTaskAsync<string>(request);

            return result.Data;
        }

        public async Task<string> RetrieveTwitterStatusesByAccountAsync(string screenName, string token)
        {
            var client = this.restClient.Create(TwitterFacadeParams.BaseUrl);
            var request = this.restRequest.Create(TwitterFacadeParams.StatusesUserTimelineEndpoint, Method.GET);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddQueryParameter("screen_name", screenName);

            var result = await client.ExecuteTaskAsync<string>(request);

            return result.Data;
        }
    }
}