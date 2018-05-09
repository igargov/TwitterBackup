using System.Collections.Generic;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Services.Contracts
{
    public interface ITwitterAccountService
    {
        TwitterAccountViewModel GetById(int accountId, int userId);

        List<TwitterAccountViewModel> GetAll(int userId);

        int Create(TwitterAccountDTO model, int userId, string picBase64);

        int Update(TwitterAccountDTO model);

        bool Delete(int accountId, int userId);

        int IsAccountPresent(string twitterId, int userId);
    }
}