using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TwitterBackupDbContext context;
        private IRepository<TwitterAccount> twitterAccounts;
        private IRepository<TwitterAccountImage> twitterAccountImages;

        public UnitOfWork(TwitterBackupDbContext context)
        {
            this.context = context;
        }

        public UnitOfWork(
            TwitterBackupDbContext context, 
            IRepository<TwitterAccount> twitterAccounts, 
            IRepository<TwitterAccountImage> twitterAccountImages)
        {
            this.context = context;
            this.twitterAccounts = twitterAccounts;
            this.twitterAccountImages = twitterAccountImages;
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

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            this.context.SaveChangesAsync();
        }
    }
}