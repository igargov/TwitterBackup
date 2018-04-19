using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly TwitterBackupDbContext context;

        public UnitOfWork(TwitterBackupDbContext context)
        {
            this.context = context;
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