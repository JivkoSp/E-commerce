using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepo Product { get; }
        IReviewRepo Review { get; }
        IReviewCommentRepo ReviewComment { get; }
        IProductCategoryRepo ProductCategory { get; }
        IPromotionCategoryRepo PromotionCategory { get; }
        IBannedUserRepo BannedUser { get; }
        IUserRepo User { get; }

        void SaveChanges();
    }
}
