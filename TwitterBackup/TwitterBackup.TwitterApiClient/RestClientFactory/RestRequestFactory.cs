using RestSharp;
using TwitterBackup.TwitterApiClient.Contracts;

namespace TwitterBackup.TwitterApiClient.RestClientFactory
{
    public class RestRequestFactory : IRestRequestFactory
    {
        public IRestRequest Create(string resource, Method method)
        {
            return new RestRequest(resource, method);
        }
    }
}
