using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class OrderRepo : Repository<Order>, IOrderRepo
    {
        private BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;

        public OrderRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }

        public IQueryable<Order> GetOrders()
        {
            return BooksPlaceDbContext.Orders;
        }
    }
}
