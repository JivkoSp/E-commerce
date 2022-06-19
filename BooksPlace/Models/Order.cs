using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models
{
    public class Order
    {
        public Order()
        {
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int OrderId { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; } 
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
