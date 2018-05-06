using TwitterBackup.TwitterDTOs;

namespace TwitterBackup.Services.Contracts
{
    public interface ITwitterStatusService
    {
        int Create(TwitterStatusDTO model);

        bool Delete();
    }
}
