using RestSharp;

namespace TwitterBackup.Providers.Contracts
{
    public interface IRestRequestFactory
    {
        RestRequest Create(string resource, Method method);
    }
}