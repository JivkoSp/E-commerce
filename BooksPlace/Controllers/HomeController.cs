using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Controllers
{
    public class HomeController : Controller
    {
        private int pageSize = 19;
        private IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(string productCategory, int pageNumber=1)
        {
            var products = unitOfWork.Product.GetViewProducts(productCategory, pageNumber, pageSize);

            if (!products.Any())
            {
                return RedirectToAction("Index", new { productCategory = productCategory, pageNumber = 1 });
            }

            return View
                (
                    new ProductViewModel
                    {
                        Products = products,
                        PageInfo = new PageInfo
                        {
                            PageNumber = pageNumber,
                            PageSize = pageSize,
                            TotalProducts = unitOfWork.Product.GetProducts(productCategory).Count()
                        }
                    }
                );
        }
    }
}
