using BooksPlace.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models.ViewModels
{
    public class EditProductViewModel
    {
        public ProductDto ProductDto { get; set; }
        //public Product Product { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
