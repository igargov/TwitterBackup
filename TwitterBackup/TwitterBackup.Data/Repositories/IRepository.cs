using System.Linq;

namespace TwitterBackup.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entiry);
    }
}