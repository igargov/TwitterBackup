using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Services
{
    public interface ITwitterAccountService
    {
        int Create(TwitterAccountDTO model);

        int Update(TwitterAccountDTO model);

        int Delete(int id);
    }
}