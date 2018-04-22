using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TwAccount> TwitterAccounts { get; }

        IRepository<TwAccountImage> TwitterAccountImages { get; }

        void SaveChanges();

        void SaveChangesAsync();
    }
}