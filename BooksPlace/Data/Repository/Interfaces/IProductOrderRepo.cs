using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IProductOrderRepo : IRepository<ProductOrder>
    {
        Dictionary<string, int> GetProductsFromOrders();
        Dictionary<string, int> GetProductNameOrders();
        Dictionary<DateTime, int> GetDateTimeOrders();
        Dictionary<string, int> GetProductCategoryOrders();
        Dictionary<string, int> GetUserPromotionCategoryOrders();
        decimal? GetStoreProffit();
    }
}
