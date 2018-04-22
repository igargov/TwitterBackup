using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly TwitterBackupDbContext context;
        private IRepository<TwAccount> twitterAccounts;
        private IRepository<TwAccountImage> twitterAccountImages;

        public UnitOfWork(TwitterBackupDbContext context)
        {
            this.context = context;
        }

        public IRepository<TwAccount> TwitterAccounts
        {
            get
            {
                if (this.twitterAccounts == null)
                {
                    return new GenericRepository<TwAccount>(this.context);
                }

                return this.twitterAccounts;
            }
        }

        public IRepository<TwAccountImage> TwitterAccountImages
        {
            get
            {
                if (this.twitterAccountImages == null)
                {
                    return new GenericRepository<TwAccountImage>(this.context);
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