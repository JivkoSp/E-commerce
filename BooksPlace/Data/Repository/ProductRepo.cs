using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        private BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;
        private IHttpContextAccessor contextAccessor;
        private UserManager<User> userManager;

        public ProductRepo(BooksPlaceDbContext dbContext, 
            IHttpContextAccessor contextAccessor, UserManager<User> userManager) :base(dbContext)
        {
            this.contextAccessor = contextAccessor;
            this.userManager = userManager;
        }
        
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return BooksPlaceDbContext.Products
                .Select(p => p.ProductCategory)
                .Distinct()
                .OrderBy(p => p.Name);
        }

        public async Task<IEnumerable<Product>> GetViewProducts(string productCategory, int pageNumber, int pageSize)
        {
            var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);

            var dbUser = BooksPlaceDbContext.Users.Where(u => u.Id == user.Id)
                         .Include(u => u.PromotionCategory)
                         .ThenInclude(u => u.Offer)
                         .FirstOrDefault();

            var promoProducts = BooksPlaceDbContext.PriceOffers
                                .Where(p => p.OfferId == dbUser.PromotionCategory.Offer.OfferId);

             var products =  BooksPlaceDbContext.Products.Where(p => productCategory == null ||
                    p.ProductCategory.Name == productCategory)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(p => p.ProductName);

            foreach(var product in products)
            {
                var priceOffer = promoProducts.FirstOrDefault(p => p.ProductId == product.ProductId);

                if(priceOffer != null)
                {
                    product.PriceOffer = priceOffer;
                }
            }

            return products;
        }

        public IEnumerable<Product> GetProducts(string productCategory)
        {
            IEnumerable<Product> products;
            if (productCategory != null)
            {
                products = BooksPlaceDbContext.Products.Where(p => p.ProductCategory.Name == productCategory);
            }
            else { products = BooksPlaceDbContext.Products; }

            return products;
        }

        public Product GetProduct(int id)
        {
            return BooksPlaceDbContext.Products.Where(p => p.ProductId == id)
                .Include(p => p.PriceOffer)
                .Include(p => p.ProductCategory).FirstOrDefault();
        }

        public List<string> SearchProductNames(string searchTerm)
        {
            return BooksPlaceDbContext.Products.Where(p => p.ProductName.Contains(searchTerm))
                    .Select(p => p.ProductName).ToList();
        }

        public int GetProductId(string productName)
        {
            return BooksPlaceDbContext.Products.Where(p => p.ProductName == productName)
                    .Select(p => p.ProductId).FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts(int productCategoryId)
        {
            return BooksPlaceDbContext.Products.Where(p => p.ProductCategoryId == productCategoryId);
        }

        public IEnumerable<Product> GetProductsOrderedByOrdersAndReviews(string productCategory)
        {
            return BooksPlaceDbContext.Products
                    .Where(p => p.ProductCategory.Name == productCategory)
                    .Include(p => p.ProductOrders)
                    .Include(p => p.Reviews)
                    .AsEnumerable()
                    .OrderByDescending(p => p.ProductOrders.Max(p => (int?)p.ProductId))
                    .ThenByDescending(p => p.Reviews.Max(p => (int?)p.ReviewId))
                    .Select(p => p);
        }
    }
}
