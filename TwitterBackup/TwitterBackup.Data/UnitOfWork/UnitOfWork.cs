using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Models.Identity;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TwitterBackupDbContext context;
        private IRepository<TwitterAccount> twitterAccounts;
        private IRepository<TwitterAccountImage> twitterAccountImages;
        private IRepository<UserTwitterAccount> userTwitterAccounts;
        private IRepository<User> users;
        private IRepository<Role> roles;

        public UnitOfWork(TwitterBackupDbContext context)
        {
            this.context = context;
        }

        public UnitOfWork(
            TwitterBackupDbContext context,
            IRepository<TwitterAccount> twitterAccounts,
            IRepository<TwitterAccountImage> twitterAccountImages,
            IRepository<UserTwitterAccount> userTwitterAccounts,
            IRepository<User> users,
            IRepository<Role> roles)
        {
            this.context = context;
            this.twitterAccounts = twitterAccounts;
            this.twitterAccountImages = twitterAccountImages;
            this.userTwitterAccounts = userTwitterAccounts;
            this.users = users;
            this.roles = roles;
        }

        public IRepository<TwitterAccount> TwitterAccounts
        {
            get
            {
                if (this.twitterAccounts == null)
                {
                    this.twitterAccounts = new GenericRepository<TwitterAccount>(this.context);
                }

                return this.twitterAccounts;
            }
        }

        public IRepository<TwitterAccountImage> TwitterAccountImages
        {
            get
            {
                if (this.twitterAccountImages == null)
                {
                    this.twitterAccountImages = new GenericRepository<TwitterAccountImage>(this.context);
                }

                return this.twitterAccountImages;
            }
        }

        public IRepository<UserTwitterAccount> UserTwitterAccounts
        {
            get
            {
                if (this.userTwitterAccounts == null)
                {
                    this.userTwitterAccounts = new GenericRepository<UserTwitterAccount>(this.context);
                }

                return this.userTwitterAccounts;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (this.users == null)
                {
                    this.users = new GenericRepository<User>(this.context);
                }

                return this.users;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (this.roles == null)
                {
                    this.roles = new GenericRepository<Role>(this.context);
                }

                return this.roles;
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}