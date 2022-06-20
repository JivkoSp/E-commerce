using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository.Interfaces
{
    public interface IOrderRepo : IRepository<Order>
    {
        IQueryable<Order> GetOrders();
    }
}
