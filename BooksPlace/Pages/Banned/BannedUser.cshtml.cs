using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksPlace.Pages.Banned
{
    public class BannedUserModel : PageModel
    {
        private IUnitOfWork unitOfWork;
        private UserManager<User> userManager;

        public DateTime DateTime { get; set; }

        public BannedUserModel(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public void OnGet()
        {
            var bann = unitOfWork.BannedUser.GetBannedUser(userManager.GetUserId(User));

            if(bann != null)
            {
                DateTime = bann.BannDate;
            }
        }
    }
}
