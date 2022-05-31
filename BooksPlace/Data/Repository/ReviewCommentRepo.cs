using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class ReviewCommentRepo : Repository<ReviewComment>, IReviewCommentRepo
    {
        public BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;

        public ReviewCommentRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }

        public bool isUserExising(string userId, int reviewId)
        {
            return BooksPlaceDbContext.ReviewComments.Where(r => r.ReviewId == reviewId)
                .FirstOrDefault(u => u.UserId == userId) != null;
        }
    }
}
