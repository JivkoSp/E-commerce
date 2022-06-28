using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IProductCategoryRepo : IRepository<ProductCategory>
    {
        ProductCategory GetProductCategory(int CategoryId);
        IEnumerable<ProductCategory> GetCategories();
    }
}
