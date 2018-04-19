using System;
using System.Collections.Generic;
using System.Text;
using TwitterBackup.Data.Models.Abstracts;

namespace TwitterBackup.Data.Repositories
{
    public interface IGenericRepository<T> where T : class, IDeletable
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entiry);
    }
}