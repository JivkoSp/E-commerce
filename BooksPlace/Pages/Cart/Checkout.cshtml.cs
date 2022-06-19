using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.ExtensionMethods;
using BooksPlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksPlace.Pages.Cart
{
    [BindProperties]
    public class CheckoutModel : PageModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string AdressLine { get; set; }

        [Required]
        public string ZipCode { get; set; }

        private UserManager<User> userManager;
        private IUnitOfWork unitOfWork;

        public CheckoutModel(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Order newOrder = new Order
                    {
                        AddressLine = AdressLine,
                        City = City,
                        Country = Country,
                        Zip = ZipCode,
                        DateTime = DateTime.Now,
                        UserId = userManager.GetUserId(User)
                    };

                    unitOfWork.Order.Add(newOrder);
                    unitOfWork.SaveChanges();

                    var cart = HttpContext.Session.GetFromSession<Models.Cart>("userCart") ?? new Models.Cart();

                    foreach (var item in cart.CartItems)
                    {
                        ProductOrder newProductOrder = new ProductOrder
                        {
                            ProductId = item.Product.ProductId,
                            OrderId = newOrder.OrderId,
                            Quantity = item.Quantity
                        };

                        unitOfWork.ProductOrder.Add(newProductOrder);
                    }

                    unitOfWork.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
