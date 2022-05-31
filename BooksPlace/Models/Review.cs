using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models
{
    public class Review
    {
        public Review()
        {
            ReviewComments = new HashSet<ReviewComment>();
        }

        public int ReviewId { get; set; }
        public string ReviewContent { get; set; }
        public byte[] ReviewImage { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ReviewComment> ReviewComments { get; set; }
    }
}
