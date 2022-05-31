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
        public IReviewRepo Review { get; private set; }
        public IReviewCommentRepo ReviewComment { get; private set; }

        public UnitOfWork(BooksPlaceDbContext dbContext, IProductRepo product, IReviewRepo review,
            IReviewCommentRepo reviewComment)
        {
            this.dbContext = dbContext;
            Product = product;
            Review = review;
            ReviewComment = reviewComment;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
