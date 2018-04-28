using System.Threading.Tasks;

namespace TwitterBackup.TwitterApiClient.Contracts
{
    public interface ITwitterApiService
    {
        Task<string> RetrieveTwitterAccountAsync(string screenName);

        Task<string> RetrieveTwitterAccountStatusesAsync(string screenName);
    }
}
