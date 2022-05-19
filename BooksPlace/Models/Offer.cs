using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models
{
    public class Offer
    {
        public Offer()
        {
            PriceOffers = new HashSet<PriceOffer>();
        }

        public int OfferId { get; set; }
        public decimal OfferPercent { get; set; }
        public int PromotionCategoryId { get; set; }
        public virtual PromotionCategory PromotionCategory { get; set; }
        public virtual ICollection<PriceOffer> PriceOffers { get; set; }
    }
}
