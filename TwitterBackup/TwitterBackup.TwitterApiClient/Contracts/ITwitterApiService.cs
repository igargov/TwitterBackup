using System.Threading.Tasks;
using TwitterBackup.TwitterApiClient.TwitterModels;

namespace TwitterBackup.TwitterApiClient.Contracts
{
    public interface ITwitterApiService
    {
        Task<TwitterAccountDTO> RetrieveTwitterAccountAsync(string screenName);

        Task<string> RetrieveTwitterAccountStatusesAsync(string screenName);
    }
}
