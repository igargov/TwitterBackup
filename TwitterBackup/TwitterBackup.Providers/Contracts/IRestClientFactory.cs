using RestSharp;

namespace TwitterBackup.Providers.Contracts
{
    public interface IRestClientFactory
    {
        RestClient Create(string baseUrl);
    }
}