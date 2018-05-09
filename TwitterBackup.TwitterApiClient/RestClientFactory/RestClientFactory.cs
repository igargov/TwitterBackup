using RestSharp;
using TwitterBackup.TwitterApiClient.Contracts;

namespace TwitterBackup.TwitterApiClient.RestClientFactory
{
    public class RestClientFactory : IRestClientFactory
    {
        public IRestClient Create(string baseUrl)
        {
            var client = new RestClient(baseUrl);
            client.AddHandler("application/json", new CustomJsonSerializer());

            return client;
        }
    }
}
