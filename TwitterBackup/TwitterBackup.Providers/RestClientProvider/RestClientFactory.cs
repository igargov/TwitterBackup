using RestSharp;
using TwitterBackup.Providers.Contracts;

namespace TwitterBackup.Providers.RestClientProvider
{
    public class RestClientFactory : IRestClientFactory
    {
        public RestClient Create(string baseUrl)
        {
            return new RestClient(baseUrl);
        }
    }
}
