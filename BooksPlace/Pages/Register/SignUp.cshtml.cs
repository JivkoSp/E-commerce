using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Enter valid username!")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "User name can contain only upper and lower case letters!")]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter valid email!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter valid password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirmed password is required!")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public SignUpModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = UserName,
                    Email = Email,
                    PromotionCategoryId = 1,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(user, Password);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Login/SignIn");
                }
            }

            return Page();
        }
    }
}
