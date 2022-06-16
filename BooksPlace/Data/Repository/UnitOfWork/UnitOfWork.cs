using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public IProductCategoryRepo ProductCategory { get; private set; }
        public IPromotionCategoryRepo PromotionCategory { get; private set; }
        public IBannedUserRepo BannedUser { get; private set; }
        public IUserRepo User { get; private set; }
        public IOrderRepo Order { get; private set; }
        public IProductOrderRepo ProductOrder { get; }

        public UnitOfWork(BooksPlaceDbContext dbContext, IProductRepo product, IReviewRepo review,
            IReviewCommentRepo reviewComment, IProductCategoryRepo productCategory,
            IPromotionCategoryRepo promotionCategory, IBannedUserRepo bannedUser, 
            IUserRepo user, IOrderRepo order, IProductOrderRepo productOrder)
        {
            this.dbContext = dbContext;
            Product = product;
            Review = review;
            ReviewComment = reviewComment;
            ProductCategory = productCategory;
            PromotionCategory = promotionCategory;
            BannedUser = bannedUser;
            User = user;
            Order = order;
            ProductOrder = productOrder;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
