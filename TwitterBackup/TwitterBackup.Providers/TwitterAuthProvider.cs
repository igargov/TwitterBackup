using RestSharp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBackup.Providers
{
    public sealed class TwitterAuthProvider
    {
        private readonly string CONSUMER_KEY;
        private readonly string CONSUMER_SECRET;
        private readonly string GRANT_TYPE = "client_credentials";
        private readonly string TOKEN_ENDPOINT = "https://api.twitter.com/oauth2/token";

        private static string accessToken = string.Empty;

        public TwitterAuthProvider(string consumer_key, string consumer_secret)
        {
            this.CONSUMER_KEY = consumer_key;
            this.CONSUMER_SECRET = consumer_secret;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (string.IsNullOrWhiteSpace(TwitterAuthProvider.accessToken))
            {
                OAuthResult authResult = await this.AcquireAccessTokenAsync();

                TwitterAuthProvider.accessToken = authResult.AccessToken;
            }

            return TwitterAuthProvider.accessToken;
        }

        private string RequestToken
        {
            get
            {
                return this.GenerateRequestToken(CONSUMER_KEY, CONSUMER_SECRET);
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
            var restClient = new RestClient(TOKEN_ENDPOINT);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Basic " + this.RequestToken);

            return (await restClient.ExecuteTaskAsync<OAuthResult>(request)).Data;
        }
    }
}
