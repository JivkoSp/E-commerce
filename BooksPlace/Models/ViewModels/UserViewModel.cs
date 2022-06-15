using BooksPlace.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models.ViewModels
{
    public class UserViewModel
    {
        public UserDto UserDto { get; set; }
        public IEnumerable<PromotionCategory> PromotionCategories { get; set; }
    }
}
