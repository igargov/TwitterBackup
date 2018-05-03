using System.Collections.Generic;
using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Services.Contracts
{
    public interface ITwitterAccountService
    {
        List<TwitterAccountWithImageViewModel> GetAll();

        int Create(TwitterAccountDTO model, int userId, string picBase64);

        int Update(TwitterAccountDTO model);

        int Delete(int id);
    }
}