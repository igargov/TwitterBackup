using RestSharp;

namespace TwitterBackup.TwitterApiClient.Contracts
{
    public interface IRestClientFactory
    {
        IRestClient Create(string baseUrl);
    }
}