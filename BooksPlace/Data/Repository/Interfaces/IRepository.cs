using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity: class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Find(Func<TEntity, bool> func);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        TEntity Get(int id);
    }
}
