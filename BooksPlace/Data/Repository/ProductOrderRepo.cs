using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class ProductOrderRepo : Repository<ProductOrder>, IProductOrderRepo
    {
        public ProductOrderRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }
    }
}
