using BooksPlace.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BooksPlace.Models.ViewModels
{
    public class ProductInformationViewModel
    {
        public ProductDto ProductDto { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
