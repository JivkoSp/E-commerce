using BooksPlace.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private BooksPlaceDbContext Context { get; }
        private UserRepository _User;

        public UnitOfWork(BooksPlaceDbContext dbContext)
        {
            Context = dbContext;
            _User = new UserRepository(Context);
        }

        public IUserRepository User => _User;

        public void Dispose()
        {
            Context.Dispose();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
