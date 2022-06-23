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
    public class UserStatusMiddleware
    {
        private RequestDelegate next;

        public UserStatusMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork, 
            UserManager<User> userManager)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(context.User);
                var orders = unitOfWork.Order.GetOrdersForUser(user.Id);
                var promotion = unitOfWork.PromotionCategory.GetPromotion(user.PromotionCategoryId);

                switch(orders.Count())
                {
                    case 10:
                        {
                            if(promotion.Name == "NormalUser")
                            {
                                user.PromotionCategoryId = 2;
                            }

                            break;
                        }
                        
                    case 30:
                        {
                            if(promotion.Name == "SilverUser")
                            {
                                user.PromotionCategoryId = 3;
                            }

                            break;
                        }           
                }

                unitOfWork.SaveChanges();
            }

            await next(context);
        }
    }
}
