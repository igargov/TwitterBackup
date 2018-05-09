using RestSharp;

namespace TwitterBackup.TwitterApiClient.Contracts
{
    public interface IRestRequestFactory
    {
        IRestRequest Create(string resource, Method method);
    }
}