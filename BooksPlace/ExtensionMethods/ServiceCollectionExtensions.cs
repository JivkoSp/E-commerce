using BooksPlace.Data.RabbitConnection;
using BooksPlace.Data.Repository;
using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Data.Repository.UnitOfWork;
using BooksPlace.MessageBroker;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IReviewRepo, ReviewRepo>();
            services.AddScoped<IReviewCommentRepo, ReviewCommentRepo>();
            services.AddScoped<IProductCategoryRepo, ProductCategoryRepo>();
            services.AddScoped<IPromotionCategoryRepo, PromotionCategoryRepo>();
            services.AddScoped<IBannedUserRepo, BannedUserRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
        }


        public static void AddRabbitMq(this IServiceCollection services)
        {
            services.AddSingleton<IBooksPlaceConnectionFactory, BooksPlaceConnectionFactory>();
            services.AddSingleton<RabbitMqHub>();
        }
    }
}
