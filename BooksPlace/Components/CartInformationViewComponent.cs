using BooksPlace.ExtensionMethods;
using BooksPlace.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Components
{
    public class CartInformationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.GetFromSession<Cart>("userCart") ?? new Cart();

            return View(cart);
        }
    }
}
