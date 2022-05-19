using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }

        public BooksPlaceDbContext dbContext
        {
            get { return Context as BooksPlaceDbContext; }
        }

        public User GetUser(int id)
        {
            return dbContext.Users.Find(id);
        }
    }
}
