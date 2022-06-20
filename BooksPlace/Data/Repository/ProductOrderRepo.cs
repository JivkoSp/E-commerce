using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class ProductOrderRepo : Repository<ProductOrder>, IProductOrderRepo
    {
        private BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;

        public ProductOrderRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }

        public Dictionary<string, int> GetProductsFromOrders()
        {
          return BooksPlaceDbContext.Orders
                .Include(o => o.User)
                .Where(o => o.User.Id == o.UserId)
                .Join(BooksPlaceDbContext.ProductOrders, order => order.OrderId, prodOrder => prodOrder.OrderId,
                (order, prodOrder) => new { Order = order, ProdOrder = prodOrder })
                .AsEnumerable()
                .GroupBy(user => user.Order.User.UserName)
                .ToDictionary(p => p.Key, p => p.Select(p => p.ProdOrder.ProductId).Count());
        }

        public Dictionary<string, int> GetProductNameOrders()
        {
            return BooksPlaceDbContext.ProductOrders
                   .Include(o => o.Product)
                   .AsEnumerable()
                   .GroupBy(o => o.Product.ProductName)
                   .ToDictionary(o => o.Key, o => o.Select(o => o.OrderId).Count());
        }

        public Dictionary<DateTime, int> GetDateTimeOrders()
        {
            return BooksPlaceDbContext.Orders
                .Join(BooksPlaceDbContext.ProductOrders, order => order.OrderId, prodOrder => prodOrder.OrderId,
                (order, prodOrder) => new { Order = order, ProdOrder = prodOrder })
                .AsEnumerable()
                .GroupBy(o => new { o.Order.DateTime.Date })
                .ToDictionary(o => o.Key.Date, o => o.Select(o => o.ProdOrder.OrderId).Count());
        }

        public Dictionary<string, int> GetProductCategoryOrders()
        {
            return BooksPlaceDbContext.ProductOrders
                    .Include(p => p.Product)
                    .ThenInclude(p => p.ProductCategory)
                    .AsEnumerable()
                    .GroupBy(p => p.Product.ProductCategory.Name)
                    .ToDictionary(p => p.Key, p => p.Select(p => p.OrderId).Count());
        }

        public Dictionary<string, int> GetUserPromotionCategoryOrders()
        {
            return BooksPlaceDbContext.Orders
                    .Include(o => o.User)
                    .ThenInclude(u => u.PromotionCategory)
                    .Join(BooksPlaceDbContext.ProductOrders, order => order.OrderId, prodOrder => prodOrder.OrderId,
                    (order, prodOrder) => new { Order = order, ProdOrder = prodOrder })
                    .AsEnumerable()
                    .GroupBy(u => u.Order.User.PromotionCategory.Name)
                    .ToDictionary(o => o.Key, o => o.Select(o => o.ProdOrder.OrderId).Count());
        }

        public decimal? GetStoreProffit()
        {
            return BooksPlaceDbContext.ProductOrders
                    .Include(p => p.Product)
                    .Sum(p => p.Product.ProductPrice * p.Quantity);
        }
    }
}
