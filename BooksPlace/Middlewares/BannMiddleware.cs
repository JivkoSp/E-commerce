using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Middlewares
{
    public class BannMiddleware
    {
        private RequestDelegate next;

        public BannMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext httpContext, IUnitOfWork unitOfWork,
            UserManager<User> userManager)
        {
            string userId = userManager.GetUserId(httpContext.User);

            var bann = unitOfWork.BannedUser.GetBannedUser(userId);

            if (bann == null || bann != null && bann.BannDate.CompareTo(DateTime.Now) <= 0)
            {
                if(bann != null)
                {
                    unitOfWork.BannedUser.Remove(bann);
                    unitOfWork.SaveChanges();
                }
            }
            else {
                httpContext.Request.Path = "/Banned/BannedUser";
            }

            await next(httpContext);
        }
    }
}
