using BooksPlace.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.GenericRepo
{
    public class Repository<TElement> : IRepository<TElement> where TElement : class
    {
        protected DbContext dbContext { get; }

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(TElement element)
        {
            dbContext.Set<TElement>().Add(element);
        }

        public void AddMany(IEnumerable<TElement> elements)
        {
            dbContext.Set<TElement>().AddRange(elements);
        }

        public void Update(TElement element)
        {
            dbContext.Set<TElement>().Update(element);
        }

        public void Remove(TElement element)
        {
            dbContext.Set<TElement>().Remove(element);
        }

        public void RemoveMany(IEnumerable<TElement> elements)
        {
            dbContext.Set<TElement>().RemoveRange(elements);
        }

        public TElement Get(int id)
        {
            return dbContext.Set<TElement>().Find(id);
        }
    }
}
