using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models
{
    public class PriceOffer
    {
        public int PriceOfferId { get; set; }
        public string PromoText { get; set; }
        public decimal NewPrice { get; set; }
        public int ProductId { get; set; }
        public int OfferId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
