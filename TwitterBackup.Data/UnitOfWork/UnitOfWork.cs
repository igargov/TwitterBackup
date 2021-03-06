﻿using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TwitterBackupDbContext context;
        private IRepository<TwitterAccount> twitterAccounts;
        private IRepository<TwitterAccountImage> twitterAccountImages;
        private IRepository<UserTwitterAccount> userTwitterAccounts;
        private IRepository<UserTwitterStatus> userTwitterStatuses;
        private IRepository<TwitterStatus> twitterStatuses;

        public UnitOfWork(TwitterBackupDbContext context)
        {
            this.context = context;
        }

        public UnitOfWork(
            TwitterBackupDbContext context,
            IRepository<TwitterAccount> twitterAccounts,
            IRepository<TwitterAccountImage> twitterAccountImages,
            IRepository<UserTwitterAccount> userTwitterAccounts,
            IRepository<UserTwitterStatus> userTwitterStatuses,
            IRepository<TwitterStatus> twitterStatuses)
        {
            this.context = context;
            this.twitterAccounts = twitterAccounts;
            this.twitterAccountImages = twitterAccountImages;
            this.userTwitterAccounts = userTwitterAccounts;
            this.userTwitterStatuses = userTwitterStatuses;
            this.twitterStatuses = twitterStatuses;
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

        public IRepository<UserTwitterStatus> UserTwitterStatuses
        {
            get
            {
                if (this.userTwitterStatuses == null)
                {
                    this.userTwitterStatuses = new GenericRepository<UserTwitterStatus>(this.context);
                }

                return this.userTwitterStatuses;
            }
        }

        public IRepository<TwitterStatus> TwitterStatuses
        {
            get
            {
                if (this.twitterStatuses == null)
                {
                    this.twitterStatuses = new GenericRepository<TwitterStatus>(this.context);
                }

                return this.twitterStatuses;
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