using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksPlace.ExtensionMethods;
using BooksPlace.Models;
using BooksPlace.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksPlace.Pages.Cart
{
    public class CartModel : PageModel
    {
        public Models.Cart Cart { get; set; }

        public void OnGet()
        {
            Cart = HttpContext.Session.GetFromSession<Models.Cart>("userCart") ?? new Models.Cart();
        }


        public IActionResult OnPostAddToCart(ProductDto ProductDto, int quantity = 1)
        {
            var cart = HttpContext.Session.GetFromSession<Models.Cart>("userCart") ?? new Models.Cart();

            if(ProductDto.NewPrice != null)
            {
                ProductDto.ProductPrice = (decimal)ProductDto.NewPrice;
            }

            cart.AddItem(ProductDto, quantity);
            HttpContext.Session.AddToSession<Models.Cart>("userCart", cart);

            return RedirectToAction("ProductView", "Product", new { productId = ProductDto.ProductId });
        }

        public JsonResult OnPostUpdateQuantity(int productId, int quantity)
        {
            var cart = HttpContext.Session.GetFromSession<Models.Cart>("userCart");
            cart.UpdateItem(productId, quantity);
            HttpContext.Session.AddToSession<Models.Cart>("userCart", cart);

            return new JsonResult(cart);
        }

        public JsonResult OnPostDeleteItem(int productId)
        {
            var cart = HttpContext.Session.GetFromSession<Models.Cart>("userCart");
            cart.DeleteItem(productId);
            HttpContext.Session.AddToSession<Models.Cart>("userCart", cart);

            string redirectUrl = Url.Page("Cart");

            return new JsonResult(new { redirectUrl });
        }
    }
}
