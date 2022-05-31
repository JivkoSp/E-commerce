using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models
{
    public class ReviewComment
    {
        public int ReviewCommentId { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public int? Likes { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int ReviewId { get; set; }
        public int? CommentId { get; set; }
        public virtual Review Review { get; set; }
        public virtual ReviewComment ParentComment { get; set; }
        public virtual ICollection<ReviewComment> ReviewComments { get; set; }
    }
}
