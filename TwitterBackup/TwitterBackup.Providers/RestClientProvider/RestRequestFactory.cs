using RestSharp;
using TwitterBackup.Providers.Contracts;

namespace TwitterBackup.Providers.RestClientFactory
{
    public class RestRequestFactory : IRestRequestFactory
    {
        public RestRequest Create(string resource, Method method)
        {
            return new RestRequest(resource, method);
        }
    }
}
