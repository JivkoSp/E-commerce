using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IUserRepo : IRepository<User>
    {
        Dictionary<string, int> GetUsersPromoCatGrouping();
        Dictionary<string, int> GetUsersReviewCount();
        Dictionary<string, int> GetUsersCommentCount();
        Dictionary<string, int> GetUsersRoleGrouping();
        List<string> SearchUserNames(string searchTerm);
        string GetUserId(string UserName);
    }
}
