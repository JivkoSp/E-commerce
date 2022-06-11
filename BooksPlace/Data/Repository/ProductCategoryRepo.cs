using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class ProductCategoryRepo : Repository<ProductCategory>, IProductCategoryRepo
    {
        private BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;

        public ProductCategoryRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }

        public ProductCategory GetProductCategory(int CategoryId)
        {
            return BooksPlaceDbContext.ProductCategories.FirstOrDefault(p => p.ProductCategoryId == CategoryId);
        }
    }
}
