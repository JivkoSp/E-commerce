using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public Dictionary<string, int> GetUsersPromoCatGrouping()
        {
            return BooksPlaceDbContext.Users
                 .Include(u => u.PromotionCategory)
                 .AsEnumerable()
                 .GroupBy(u => u.PromotionCategory.Name)
                 .ToDictionary(u => u.Key, u => u.Select(x => x.UserName).Count());
        }

        public Dictionary<string, int> GetUsersReviewCount()
        {
            return BooksPlaceDbContext.Users
                   .Include(u => u.Reviews)
                   .Where(u => u.Reviews.Count > 0)
                   .AsEnumerable()
                   .GroupBy(u => u.UserName)
                   .ToDictionary(u => u.Key, u => u.Select(u => u.Reviews.Count).FirstOrDefault());
        }

        public Dictionary<string, int> GetUsersCommentCount()
        {
            return BooksPlaceDbContext.Users
                    .Include(u => u.ReviewComments)
                    .Where(u => u.ReviewComments.Count > 0)
                    .AsEnumerable()
                    .GroupBy(u => u.UserName)
                    .ToDictionary(u => u.Key, u => u.Select(u => u.ReviewComments.Count).FirstOrDefault());
        }

        public Dictionary<string, int> GetUsersRoleGrouping()
        {
            return BooksPlaceDbContext.Roles
                 .Join(BooksPlaceDbContext.UserRoles, role => role.Id, userRole => userRole.RoleId,
                 (role, userRole) => new { Role = role, UserRole = userRole })
                 .Join(BooksPlaceDbContext.Users, role => role.UserRole.UserId, user => user.Id,
                 (role, user) => new { Role = role, User = user})
                     .AsEnumerable()
                     .GroupBy(u => u.User.UserName)
                     .ToDictionary(u => u.Key, u => u.Select(u => u.Role.UserRole.RoleId).Count());
        }
    }
}
