using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models
{
    public class BannedUser
    {
        public int BannedUserId { get; set; }
        public string UserId { get; set; }
        public DateTime BannDate { get; set; }
        public virtual User User { get; set; }
    }
}
