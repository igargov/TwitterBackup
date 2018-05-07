using System.Collections.Generic;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Services.Contracts
{
    public interface ITwitterStatusService
    {
        List<TwitterStatusViewModel> GetAll(int accountId);

        int Create(TwitterStatusDTO model);

        bool Delete();
    }
}
