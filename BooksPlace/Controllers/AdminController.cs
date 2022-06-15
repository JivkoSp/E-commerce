using AutoMapper;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.ExtensionMethods;
using BooksPlace.Models;
using BooksPlace.Models.Dtos;
using BooksPlace.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;

        public AdminController(IUnitOfWork unitOfWork, IMapper mapper, 
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

        public IActionResult Manage_Customers()
        {
            List<UserDto> userDtos = new List<UserDto>();

            foreach(var user in userManager.Users)
            {
                userDtos.Add(mapper.Map<UserDto>(user));
            }

            return View
                (
                    new UsersViewModel
                    {
                        UserDtos = userDtos,
                        PromotionCategories = unitOfWork.PromotionCategory.GetPromotionCategories()
                    }
                );
        }

        public async Task<IActionResult> Edit_Customer(string customerId)
        {
            return View
                (
                    new UserViewModel
                    {
                        UserDto = mapper.Map<UserDto>(await userManager.FindByIdAsync(customerId)),
                        PromotionCategories = unitOfWork.PromotionCategory.GetPromotionCategories()
                    }
                );
        }

        [HttpPost]
        public async Task<IActionResult> Edit_Customer(UserDto UserDto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Models.User user = mapper.Map<User>(UserDto);

                    unitOfWork.User.Attach(user);

                    await userManager.AddToRoleAsync(user, UserDto.UserRole);

                    if (UserDto.BanUser)
                    {
                        var bannedUser = unitOfWork.BannedUser.GetBannedUser(UserDto.Id);

                        if(bannedUser != null)
                        {
                            bannedUser.BannDate = UserDto.BanDateTime;
                            unitOfWork.BannedUser.Update(bannedUser);
                        }
                        else
                        {
                            BannedUser newBannedUser = new BannedUser
                            {
                                UserId = UserDto.Id,
                                BannDate = UserDto.BanDateTime
                            };

                            unitOfWork.BannedUser.Add(newBannedUser);
                        }
                    }

                    if(UserDto.UnBanUser)
                    {
                        var bannedUser = unitOfWork.BannedUser.GetBannedUser(UserDto.Id);

                        if(bannedUser != null)
                        {
                            unitOfWork.BannedUser.Remove(bannedUser);
                        }
                    }

                    await userManager.UpdateAsync(user);
                    unitOfWork.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return RedirectToAction("Manage_Customers");
            }

            return View
                (
                     new UserViewModel
                     {
                         UserDto = UserDto,
                         PromotionCategories = unitOfWork.PromotionCategory.GetPromotionCategories()
                     }
                );
        }

        public async Task<IActionResult> Delete_Customer(string customerId)
        {
            await userManager.DeleteAsync(await userManager.FindByIdAsync(customerId));
            return RedirectToAction("Manage_Customers");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            try
            {
                IdentityRole newRole = new IdentityRole
                {
                    Name = roleName
                };

                await roleManager.CreateAsync(newRole);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Manage_Customers");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto UserDto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Models.User newUser = new Models.User
                    {
                        UserName = UserDto.UserName,
                        Email = UserDto.Email,
                        EmailConfirmed = true,
                        PhoneNumber = UserDto.PhoneNumber,
                        PromotionCategoryId = UserDto.PromotionCategoryId
                    };

                    IdentityResult result = await userManager.CreateAsync(newUser, UserDto.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, UserDto.UserRole);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return RedirectToAction("Manage_Customers");
        }
    }
}
