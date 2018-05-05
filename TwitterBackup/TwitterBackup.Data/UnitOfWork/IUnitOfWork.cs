using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TwitterAccount> TwitterAccounts { get; }

        IRepository<TwitterAccountImage> TwitterAccountImages { get; }

        IRepository<UserTwitterAccount> UserTwitterAccounts { get; }

        IRepository<User> Users { get; }

        IRepository<Role> Roles { get; }

        IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken> Context { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}