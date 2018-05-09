using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TwitterAccount> TwitterAccounts { get; }

        IRepository<TwitterAccountImage> TwitterAccountImages { get; }

        IRepository<UserTwitterAccount> UserTwitterAccounts { get; }

        IRepository<UserTwitterStatus> UserTwitterStatuses { get; }

        IRepository<TwitterStatus> TwitterStatuses { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}