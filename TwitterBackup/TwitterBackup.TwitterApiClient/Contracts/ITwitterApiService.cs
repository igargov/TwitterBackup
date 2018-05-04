using System.Threading.Tasks;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.TwitterApiClient.Contracts
{
    public interface ITwitterApiService
    {
        Task<TwitterAccountDTO> RetrieveTwitterAccountAsync(string screenName);

        Task<string> RetrieveTwitterAccountStatusesAsync(string screenName);

        Task<string> RetrieveAccountProfileImage(string url);
    }
}
