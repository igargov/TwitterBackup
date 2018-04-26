using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TwitterBackup.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly TwitterBackupDbContext context;

        public GenericRepository(TwitterBackupDbContext context)
        {
            this.context = context;
        }

        public T GetById(int id)
        {
            var entity = this.context.DbSet<T>().Find(id);

            return entity;
        }
        
        public void Add(T entity)
        {
            EntityEntry entityEntry = this.context.Entry(entity);

            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }

            this.context.Add(entity);
        }

        public void Update(T entity)
        {
            EntityEntry entityEntry = this.context.Entry(entity);

            if (entityEntry.State == EntityState.Detached)
            {
                this.context.DbSet<T>().Attach(entity);
            }

            entityEntry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            EntityEntry entry = this.context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.context.DbSet<T>().Attach(entity);
                this.context.DbSet<T>().Remove(entity);
            }
        }
    }
}