using System.Threading.Tasks;

namespace TwitterBackup.Providers.Contracts
{
    public interface ITwitterFacadeProvider
    {
        Task<string> RetrieveTwitterAccountAsync(string screenName, string token);

        Task<string> RetrieveTwitterStatusesByAccountAsync(string screenName, string token);
    }
}
