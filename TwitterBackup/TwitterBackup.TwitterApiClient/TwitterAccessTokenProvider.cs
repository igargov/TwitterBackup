using RestSharp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBackup.TwitterApiClient
{
    public class TwitterAccessTokenProvider
    {
        private readonly string consumer_key;
        private readonly string consumer_secret;

        private TwitterAuthResultModel authResult;
        private string requestToken = string.Empty;

        public TwitterAccessTokenProvider(string consumer_key, string consumer_secret)
        {
            this.consumer_key = consumer_key;
            this.consumer_secret = consumer_secret;
        }

        public async Task<TwitterAuthResultModel> GetAccessTokenAsync()
        {
            if (authResult == null)
            {
                this.authResult = await this.AcquireAccessTokenAsync();
            }

            return authResult;
        }

        private string RequestToken
        {
            get
            {
                if (string.IsNullOrWhiteSpace(requestToken))
                {
                    requestToken = this.GenerateRequestToken(consumer_key, consumer_secret);
                }

                return requestToken;
            }
        }

        private string GenerateRequestToken(string key, string secret)
        {
            string concatStr = string.Join(':', key, secret);

            byte[] bytes = Encoding.UTF8.GetBytes(concatStr);

            var encodedStr = Convert.ToBase64String(bytes);

            return encodedStr;
        }

        private async Task<TwitterAuthResultModel> AcquireAccessTokenAsync()
        {
            var restClient = new RestClient(TwitterApiParams.TokenEndpoint);

            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", TwitterApiParams.GrantType);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Basic " + this.RequestToken);

            var result = await restClient.ExecuteTaskAsync<TwitterAuthResultModel>(request);

            return result.Data;
        }
    }
}
