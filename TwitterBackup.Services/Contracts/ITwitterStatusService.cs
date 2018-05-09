using System.Collections.Generic;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.Models;

namespace TwitterBackup.Services.Contracts
{
    public interface ITwitterStatusService
    {
        List<TwitterStatusViewModel> GetAll(int userId);

        List<TwitterStatusViewModel> GetAll(int accountId, int userId);

        int Create(TwitterStatusDTO model, int userId);

        bool Delete(int statusId, int userId);
    }
}
