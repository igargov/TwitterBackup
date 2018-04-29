using TwitterBackup.Services.ViewModels;
using TwitterBackup.TwitterApiClient.TwitterModels;

namespace TwitterBackup.Services
{
    public interface ITwitterAccountService
    {
        int SaveTwitterAccount(TwitterAccountDTO model);
    }
}