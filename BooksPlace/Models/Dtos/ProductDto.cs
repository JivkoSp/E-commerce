using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models.Dtos
{
    #region Comment

       //This DTO have two properties representing image in two states
       // => State 1- [IFormFile] for binding from UI
       // => State 2- [byte[]] for retrieving from database
       //This is to prevent multiple convertions from one type to another 
       //i.e from byte to FromFile or oposite

    #endregion

    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        [Required]
        public IFormFile PictureFile { get; set; }

        public byte[] ProductImage { get; set; }

        public int ProductCategoryId { get; set; }

        public bool isUpdate { get; set; }

        public string PromoText { get; set; }

        public decimal? NewPrice { get; set; }
    }
}
