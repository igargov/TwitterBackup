using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.TwitterModels;

namespace TwitterBackup.Services
{
    public interface ITwitterAccountService
    {
        int Create(TwitterAccountDTO model);

        int Update(TwitterAccountDTO model);

        int Delete(int id);
    }
}