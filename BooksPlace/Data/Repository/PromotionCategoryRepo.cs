using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class PromotionCategoryRepo : Repository<PromotionCategory>, IPromotionCategoryRepo
    {
        private BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;

        public PromotionCategoryRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }

        public IEnumerable<PromotionCategory> GetPromotionCategories()
        {
            return BooksPlaceDbContext.PromotionCategories;
        }

        public PromotionCategory GetPromotion(int promotionId)
        {
            return BooksPlaceDbContext.PromotionCategories.Find(promotionId);
        }
    }
}
