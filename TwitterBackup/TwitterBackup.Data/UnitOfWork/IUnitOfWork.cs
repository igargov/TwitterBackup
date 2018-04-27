﻿using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repositories;

namespace TwitterBackup.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TwitterAccount> TwitterAccounts { get; }

        IRepository<TwitterAccountImage> TwitterAccountImages { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}