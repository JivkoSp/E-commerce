using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IReviewRepo : IRepository<Review>
    {
        IEnumerable<Review> GetReviews(int productId);
        Review GetReview(int reviewId);
        bool isUserExising(string userId, int productId);
    }
}
