using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class BannedUserRepo : Repository<BannedUser>, IBannedUserRepo
    {
        private BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;

        public BannedUserRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }

        public BannedUser GetBannedUser(string userId)
        {
            return BooksPlaceDbContext.BannedUsers.FirstOrDefault(u => u.UserId == userId);
        }
    }
}
