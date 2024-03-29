﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            ReviewComments = new HashSet<ReviewComment>();
        }

        public int PromotionCategoryId { get; set; }
        public virtual BannedUser BannedUser { get; set; }
        public virtual PromotionCategory PromotionCategory { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection <ReviewComment> ReviewComments { get; set; }
    }
}
