using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackup.Data.Models.Abstracts;

namespace TwitterBackup.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IDeletable
    {
        private readonly TwitterBackupDbContext context;

        public GenericRepository(TwitterBackupDbContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            EntityEntry entityEntry = this.context.Entry(entity);

            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }

            this.context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            entity.isDeleted = true;
            entity.DeletedOn = DateTime.Now;

            EntityEntry entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Update(T entity)
        {
            EntityEntry entityEntry = this.context.Entry(entity);

            if (entityEntry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entityEntry.State = EntityState.Modified;
        }
    }
}