using RestSharp;
using TwitterBackup.TwitterApiClient.Contracts;

namespace TwitterBackup.TwitterApiClient.RestClientFactory
{
    public class RestClientFactory : IRestClientFactory
    {
        public IRestClient Create(string baseUrl)
        {
            return new RestClient(baseUrl);
        }
    }
}
