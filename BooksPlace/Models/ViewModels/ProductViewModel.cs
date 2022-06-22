using BooksPlace.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
