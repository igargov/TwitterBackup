using RestSharp;
using TwitterBackup.Providers.Contracts;

namespace TwitterBackup.Providers.RestClientFactory
{
    public class RestClientFactory : IRestClientFactory
    {
        public RestClient Create(string baseUrl)
        {
            return new RestClient(baseUrl);
        }
    }
}
