using System.Threading.Tasks;
using TwitterBackup.TwitterApiClient.Models;

namespace TwitterBackup.TwitterApiClient.Contracts
{
    public interface ITwitterApiService
    {
        Task<TwitterAccountDTO> RetrieveTwitterAccountAsync(string screenName);

        Task<TwitterStatusDTO> RetrieveTwitterStatusAsync(string statusId);

        Task<TwitterStatusesDTO> RetrieveTwitterAccountStatusesAsync(string screenName);

        Task<TwitterStatusesDTO> RetrieveTwitterAccountStatusesAsync(string screenName, int count);

        Task<string> RetrieveAccountProfileImage(string url);
    }
}
