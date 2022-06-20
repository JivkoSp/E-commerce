using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models.Dtos
{
    public class PromotionDto
    {
        [Required]
        public string PromotionName { get; set; }
        public string PromotionCode { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
    }
}
