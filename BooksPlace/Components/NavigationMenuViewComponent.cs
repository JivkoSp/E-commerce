using BooksPlace.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IUnitOfWork unitOfWork;

        public NavigationMenuViewComponent(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke()
        {
            return View(unitOfWork.Product.GetProductCategories());
        }
    }
}
