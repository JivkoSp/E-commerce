using AutoMapper;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.ExtensionMethods;
using BooksPlace.Models;
using BooksPlace.Models.Dtos;
using BooksPlace.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Controllers
{
    public class AdminController : Controller
    {
        private int pageSize = 19;
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public AdminController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalog_Categories()
        {
            return View();
        }

        public IActionResult Catalog_Products(int pageNumber = 1)
        {
            var products = unitOfWork.Product.GetViewProducts(null, pageNumber, pageSize);

            return View
                (
                    new ProductViewModel
                    {
                        Products = products,
                        PageInfo = new PageInfo
                        {
                            PageNumber = pageNumber,
                            PageSize = pageSize,
                            TotalProducts = unitOfWork.Product.GetProducts(null).Count()
                        }
                    }
                );
        }

        public IActionResult EditProduct(int productId)
        {
          return View
                (
                    new EditProductViewModel
                    {
                        ProductDto = mapper.Map<ProductDto>(unitOfWork.Product.GetProduct(productId)),
                        ProductCategories = unitOfWork.Product.GetProductCategories()
                    }
                );
        }

        [HttpPost]
        public IActionResult EditProduct(ProductDto product)
        { 
            if(ModelState.IsValid)
            {
                Product newProduct = mapper.Map<Product>(product);
                newProduct.ProductCategory = unitOfWork.ProductCategory.GetProductCategory(product.ProductCategoryId);

                if (product.isUpdate)
                {
                    unitOfWork.Product.Update(newProduct);
                }
                else { unitOfWork.Product.Add(newProduct); }
  
                unitOfWork.SaveChanges();

                product = mapper.Map<ProductDto>(newProduct);
            }

             return View
                 (
                    new EditProductViewModel
                    {
                        ProductDto = product,
                        ProductCategories = unitOfWork.Product.GetProductCategories()
                    }
                 );
        }

    }
}
