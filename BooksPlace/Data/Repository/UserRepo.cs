using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class UserRepo : Repository<User>, IUserRepo
    {
        private BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;

        public UserRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }

        public void AttachToIdentityContext(User user)
        {
            BooksPlaceDbContext.Attach(user);
        }
    }
}
