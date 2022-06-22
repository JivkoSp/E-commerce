using BooksPlace.Models.Dtos;
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
            public ProductDto Product { get; set; }
            public int Quantity { get; set; }
        }

        public List<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public void AddItem(ProductDto product, int quantity)
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
        
        public void UpdateItem(int productId, int quantity)
        {
            try
            {
                var cartItem = CartItems.FirstOrDefault(i => i.Product.ProductId == productId);

                cartItem.Quantity = quantity;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteItem(int productId)
        {
            try
            {
                var cartItem = CartItems.FirstOrDefault(i => i.Product.ProductId == productId);
                CartItems.Remove(cartItem);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public decimal TotalPrice => CartItems.Sum(i => i.Product.ProductPrice * i.Quantity);

    }
}
