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
    public class ReviewRepo : Repository<Review>, IReviewRepo
    {
        private BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;

        public ReviewRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }

        public IEnumerable<Review> GetReviews(int productId)
        {
            return BooksPlaceDbContext.Reviews.Where(p => p.ProductId == productId).Include(p => p.User)
                .Include(p => p.ReviewComments)
                .ThenInclude(rc => rc.ReviewComments)
                .ThenInclude(rc => rc.User);
                
        }

        public Review GetReview(int reviewId)
        {
            return BooksPlaceDbContext.Reviews.Find(reviewId);
        }

        public bool isUserExising(string userId, int productId)
        {
            return BooksPlaceDbContext.Reviews.Where(p => p.ProductId == productId)
                    .FirstOrDefault(u => u.UserId == userId) != null;
        }
    }
}
