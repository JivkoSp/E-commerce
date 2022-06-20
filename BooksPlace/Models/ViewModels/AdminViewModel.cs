using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models.ViewModels
{
    public class AdminViewModel
    {
        public int UsersCount { get; set; }
        public int OrderCount { get; set; }
        public int ProductReviewsCount { get; set; }
        public decimal? StoreProffit { get; set; }
    }
}
