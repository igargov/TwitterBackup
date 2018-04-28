using RestSharp;
using TwitterBackup.Providers.Contracts;

namespace TwitterBackup.Providers.RestClientProvider
{
    public class RestRequestFactory : IRestRequestFactory
    {
        public RestRequest Create(string resource, Method method)
        {
            return new RestRequest(resource, method);
        }
    }
}
