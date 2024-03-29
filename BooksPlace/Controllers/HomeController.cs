﻿using AutoMapper;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using BooksPlace.Models.Dtos;
using BooksPlace.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private int pageSize = 19;
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
     
        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string productCategory, int pageNumber=1)
        {
            var products = await unitOfWork.Product.GetViewProducts(productCategory, pageNumber, pageSize);

            if (!products.Any())
            {
                return RedirectToAction("Index", new { productCategory = productCategory, pageNumber = 1 });
            }

            return View
                (
                    new ProductViewModel
                    {
                        Products = mapper.Map<IEnumerable<ProductDto>>(products),
                        PageInfo = new PageInfo
                        {
                            PageNumber = pageNumber,
                            PageSize = pageSize,
                            TotalProducts = unitOfWork.Product.GetProducts(productCategory).Count()
                        }
                    }
                );
        }

        [HttpPost]
        public IActionResult SearchResult(string ProductName)
        {
            int productId = 0;
            try
            {
                productId =  unitOfWork.Product.GetProductId(ProductName);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction("ProductView", "Product", new { productId = productId });
        }
    }
}
