using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;
using System.Threading.Tasks;
using BooksPlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BooksPlace.Data.RabbitConnection;
using BooksPlace.Data.Repository.Interfaces;

namespace BooksPlace.Pages.Login
{
    public class SignInModel : PageModel
    {
        private SignInManager<User> signInManager;
        private IBooksPlaceConnectionFactory factory;

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public SignInModel(SignInManager<User> signInManager, IBooksPlaceConnectionFactory factory)
        {
            this.signInManager = signInManager;
            this.factory = factory;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var res = await signInManager.PasswordSignInAsync(UserName, Password, false, false);

            if(res.Succeeded)
            {
                var connection = factory.GetConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare
                    (
                        queue: UserName,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null
                    );

                channel.ExchangeDeclare
                    (
                        exchange: "BooksPlaceExchange",
                        type: "direct",
                        durable: false,
                        autoDelete: false,
                        arguments: null
                    );

                channel.QueueBind(queue: UserName, exchange: "BooksPlaceExchange", routingKey: "key1", arguments: null);

                return Redirect("/");
            }

            return Page();
        }
    }
}
