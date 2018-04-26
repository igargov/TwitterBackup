using RestSharp;
using System;
using System.Text;
using System.Threading.Tasks;
using TwitterBackup.Providers.TwitterProviders;

namespace TwitterBackup.Providers
{
    public sealed class TwitterOAuthProvider
    {
        private readonly string consumer_key;
        private readonly string consumer_secret;

        private static string accessToken = string.Empty;
        private static string requestToken = string.Empty;

        public TwitterOAuthProvider(string consumer_key, string consumer_secret)
        {
            this.consumer_key = consumer_key;
            this.consumer_secret = consumer_secret;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (string.IsNullOrWhiteSpace(TwitterOAuthProvider.accessToken))
            {
                OAuthResult authResult = await this.AcquireAccessTokenAsync();

                TwitterOAuthProvider.accessToken = authResult.AccessToken;
            }

            return TwitterOAuthProvider.accessToken;
        }

        private string RequestToken
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TwitterOAuthProvider.requestToken))
                {
                    TwitterOAuthProvider.requestToken = this.GenerateRequestToken(consumer_key, consumer_secret);
                }

                return TwitterOAuthProvider.requestToken;
            }
        }

        private string GenerateRequestToken(string key, string secret)
        {
            string concatStr = string.Join(':', key, secret);

            byte[] bytes = Encoding.UTF8.GetBytes(concatStr);

            var encodedStr = Convert.ToBase64String(bytes);

            return encodedStr;
        }

        private async Task<OAuthResult> AcquireAccessTokenAsync()
        {
            var restClient = new RestClient(TwitterFacadeParams.TokenEndpoint);

            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", TwitterFacadeParams.GrantType);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Basic " + this.RequestToken);

            var result = await restClient.ExecuteTaskAsync<OAuthResult>(request);

            return result.Data;
        }
    }
}
