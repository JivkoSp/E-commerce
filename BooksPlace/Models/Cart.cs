using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Models
{
    public class Cart
    {
        public class CartItem
        { 
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }

        public List<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            try
            {
                var cartItem = CartItems.FirstOrDefault(p => p.Product.ProductId == product.ProductId);

                if (cartItem == null)
                {
                    CartItems.Add(new CartItem { Product = product, Quantity = quantity });
                }
                else
                {
                    cartItem.Quantity += quantity;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
