using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IReviewCommentRepo : IRepository<ReviewComment>
    {
        bool isUserExising(string userId, int reviewId);
    }
}
