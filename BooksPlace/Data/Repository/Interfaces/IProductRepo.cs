using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IProductRepo : IRepository<Product>
    {
        IEnumerable<ProductCategory> GetProductCategories();
        Task<IEnumerable<Product>> GetViewProducts(string productCategory, int pageNumber, int pageSize);
        IEnumerable<Product> GetProducts(string productCategory);
        IEnumerable<Product> GetProductsOrderedByOrdersAndReviews(string productCategory);
        IEnumerable<Product> GetProducts(int productCategoryId);
        Product GetProduct(int id);
        int GetProductId(string productName);
        List<string> SearchProductNames(string searchTerm);
    }
}
