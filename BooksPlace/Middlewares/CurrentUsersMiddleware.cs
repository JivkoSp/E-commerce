using BooksPlace.Models;
using BooksPlace.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Middlewares
{
    public class CurrentUsersMiddleware
    {
        private RequestDelegate next;

        public CurrentUsersMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context, UserManager<User> userManager)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                TrackUsers.UpdateUserAccess(userManager.GetUserId(context.User));
            }

            await next(context);
        }
    }
}
