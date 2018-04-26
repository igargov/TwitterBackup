using TwitterBackup.Services.ViewModels;

namespace TwitterBackup.Services
{
    public interface ITwitterAccountService
    {
        int SaveAccount(TwitterAccountViewModel model);
    }
}
