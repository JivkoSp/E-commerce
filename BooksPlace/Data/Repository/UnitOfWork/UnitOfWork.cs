using BooksPlace.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private BooksPlaceDbContext dbContext;
        public IProductRepo Product { get; private set; }

        public UnitOfWork(BooksPlaceDbContext dbContext, IProductRepo product)
        {
            this.dbContext = dbContext;
            Product = product;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
