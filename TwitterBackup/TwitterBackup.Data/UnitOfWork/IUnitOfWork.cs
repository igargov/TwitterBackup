using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TwitterAccount> TwitterAccounts { get; }

        IRepository<TwitterAccountImage> TwitterAccountImages { get; }

        void SaveChanges();

        void SaveChangesAsync();
    }
}