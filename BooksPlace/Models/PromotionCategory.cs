using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models
{
    public class PromotionCategory
    {
        public PromotionCategory()
        {
            Users = new HashSet<User>();
        }

        public int PromotionCategoryId { get; set; }
        public string Name { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
