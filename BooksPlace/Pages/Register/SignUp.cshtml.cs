using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksPlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksPlace.Pages.Register
{
    public class SignUpModel : PageModel
    {
        private UserManager<User> userManager;

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public SignUpModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            User user = new User
            {
                UserName = UserName,
                Email = Email,
                PromotionCategoryId = 1,
                EmailConfirmed = true
            };

            IdentityResult result =  await userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                return RedirectToPage("/Login/SignIn");
            }

            return Page();
        }
    }
}
