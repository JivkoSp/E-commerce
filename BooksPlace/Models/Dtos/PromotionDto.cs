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
        public string PromotionText { get; set; }
        [Required]
        public string PromotionCode { get; set; }
        public string ProductName { get; set; }
        [Required]
        public int? OfferId { get; set; }
        public int ProductCategoryId { get; set; }
        [Required]
        public decimal? PromotionPercent { get; set; }
        public IEnumerable<PromotionCategory> PromotionCategories { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
