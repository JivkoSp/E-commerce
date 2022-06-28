using AutoMapper;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.ExtensionMethods;
using BooksPlace.Models;
using BooksPlace.Models.Dtos;
using BooksPlace.Models.ViewModels;
using BooksPlace.Static;
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
    [Authorize(Policy = "AdminControllerAuthPolicy")]
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
            return View
                (
                    new AdminViewModel
                    {
                        UsersCount = userManager.Users.Count(),
                        OrderCount = unitOfWork.Order.GetOrders().Count(),
                        ProductReviewsCount = unitOfWork.Review.GetProductReviews(),
                        StoreProffit = unitOfWork.ProductOrder.GetStoreProffit()
                    }
                );
        }

        [HttpGet]
        public JsonResult GetOrderChartData()
        {
            var orderedProducts = unitOfWork.ProductOrder.GetProductsFromOrders();

            var chartData = new object[orderedProducts.Count() + 1];

            chartData[0] = new object[]
            {
                "UserName",
                "Products"
            };

            int indx = 1;
            foreach (var product in orderedProducts)
            {
                chartData[indx] = new object[]
                {
                    product.Key,
                    product.Value
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        [HttpGet]
        public JsonResult GetProductNameOrderChartData()
        {
            var data = unitOfWork.ProductOrder.GetProductNameOrders();

            var chartData = new object[data.Count() + 1];

            chartData[0] = new object[]
            {
                "ProductName",
                "Orders"
            };

            int indx = 1;
            foreach(var item in data)
            {
                chartData[indx] = new object[]
                {
                    item.Key,
                    item.Value
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        [HttpGet]
        public JsonResult GetDateTimeOrderChartData()
        {
            var data = unitOfWork.ProductOrder.GetDateTimeOrders();

            var chartData = new object[data.Count() + 1];

            chartData[0] = new object[]
            {
                "DateTime",
                "Products"
            };

            int indx = 1;
            foreach (var item in data)
            {
                chartData[indx] = new object[]
                {
                    item.Key.ToString("dd/MM/yyyy"),
                    item.Value
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        [HttpGet]
        public IActionResult GetProductCategoryOrderChartData()
        {
            var data = unitOfWork.ProductOrder.GetProductCategoryOrders();

            var chartData = new object[data.Count() + 1];

            chartData[0] = new object[]
            {
                "ProductCategory",
                "Orders"
            };

            int indx = 1;
            foreach (var item in data)
            {
                chartData[indx] = new object[]
                {
                    item.Key,
                    item.Value
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        [HttpGet]
        public IActionResult GetUserPromotionCategoryOrderChartData()
        {
            var data = unitOfWork.ProductOrder.GetUserPromotionCategoryOrders();

            var chartData = new object[data.Count() + 1];

            chartData[0] = new object[]
            {
                "UserPromoCat",
                "Orders"
            };

            int indx = 1;
            foreach (var item in data)
            {
                chartData[indx] = new object[]
                {
                    item.Key,
                    item.Value
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        [HttpGet]
        public JsonResult GetUserPromoGatChartData()
        {
            var promoCatGrouping = unitOfWork.User.GetUsersPromoCatGrouping();

            var chartData = new object[promoCatGrouping.Count() + 1];

            chartData[0] = new object[]
             {
                "UserPromoCat",
                "Users"
             };

            int indx = 1;

            foreach(var grouping in promoCatGrouping)
            {
                chartData[indx] = new object[]
                {
                    grouping.Key,
                    grouping.Value
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        [HttpGet]
        public JsonResult GetUsersReviewChartData()
        {
            var usersReviewCount = unitOfWork.User.GetUsersReviewCount();

            var chartData = new object[usersReviewCount.Count() + 1];

            chartData[0] = new object[]
            {
                "UserName",
                "Reviews"
            };

            int indx = 1;

            foreach(var grouping in usersReviewCount)
            {
                chartData[indx] = new object[]
                {
                    grouping.Key,
                    grouping.Value
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        [HttpGet]
        public JsonResult GetUsersCommentChartData()
        {
            var usersCommentCount = unitOfWork.User.GetUsersCommentCount();

            var chartData = new object[usersCommentCount.Count() + 1];

            chartData[0] = new object[]
            {
                "UserName",
                "Comments"
            };

            int indx = 1;

            foreach (var grouping in usersCommentCount)
            {
                chartData[indx] = new object[]
                {
                    grouping.Key,
                    grouping.Value
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        [HttpGet]
        public JsonResult GetUsersRoleChartData()
        {
            var usersRoleData = unitOfWork.User.GetUsersRoleGrouping();

            var chartData = new object[usersRoleData.Count() + 1];

            chartData[0] = new object[]
            {
                "UserName",
                "Role"
            };

            int indx = 1;

            foreach (var grouping in usersRoleData)
            {
                chartData[indx] = new object[]
                {
                    grouping.Key,
                    grouping.Value
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        [HttpGet]
        public JsonResult GetCurrentUsersChartData()
        {
            var data = TrackUsers.GetActiveUserHistory();

            var chartData = new object[data.Count() + 1];

            chartData[0] = new object[]
            {
                "Minutes",
                "Users"
            };

            int indx = 1;

            foreach (var grouping in data)
            {
                chartData[indx] = new object[]
                {
                    grouping.Value.FirstOrDefault().Value.ToString("HH:mm"),
                    grouping.Value.Count
                };
                indx++;
            }

            return new JsonResult(chartData);
        }

        public IActionResult Catalog_Categories()
        {
            return View();
        }

        public async Task<IActionResult> Catalog_Products(int pageNumber = 1)
        {
            var products = await unitOfWork.Product.GetViewProducts(null, pageNumber, pageSize);

            return View
                (
                    new ProductViewModel
                    {
                        Products = mapper.Map<IEnumerable<ProductDto>>(products),
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
                        ProductCategories = unitOfWork.ProductCategory.GetCategories()
                    }
                );
        }

        [HttpPost]
        public IActionResult EditProduct(ProductDto ProductDto)
        { 
            if(ModelState.IsValid)
            {
                Product newProduct = mapper.Map<Product>(ProductDto);
                newProduct.ProductCategory = unitOfWork.ProductCategory.GetProductCategory(ProductDto.ProductCategoryId);

                if (ProductDto.isUpdate)
                {
                    unitOfWork.Product.Update(newProduct);
                }
                else { unitOfWork.Product.Add(newProduct); }
  
                unitOfWork.SaveChanges();

                ProductDto = mapper.Map<ProductDto>(newProduct);
            }

             return View
                 (
                    new EditProductViewModel
                    {
                        ProductDto = ProductDto,
                        ProductCategories = unitOfWork.Product.GetProductCategories()
                    }
                 );
        }

        [Authorize(Roles = "Admin")]
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

        [HttpPost]
        public IActionResult SearchResult(string UserName)
        {
            string customerId;
            try
            {
                customerId = unitOfWork.User.GetUserId(UserName);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction("Edit_Customer", new { customerId = customerId });
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

        public IActionResult Manage_Promotions()
        {
            return View
                (
                    new PromotionDto 
                    { 
                        PromotionCategories = unitOfWork.PromotionCategory.GetPromotionCategories(),
                        ProductCategories = unitOfWork.Product.GetProductCategories()
                    }
                );
        }

        [HttpPost]
        public IActionResult Manage_Promotions(PromotionDto PromotionDto)
        {
            if(ModelState.IsValid)
            {
                PromotionDto.PromotionPercent /= 100;

                if (!string.IsNullOrEmpty(PromotionDto.ProductName))
                {
                    int productId = unitOfWork.Product.GetProductId(PromotionDto.ProductName);

                    if(productId == 0)
                    {
                        ModelState.AddModelError("productNotExisting", "This product does not exist!");
                    }
                    else
                    {
                        var product = unitOfWork.Product.GetProduct(productId);

                        PriceOffer newPriceOffer = new PriceOffer
                        {
                            PromoText = PromotionDto.PromotionText,
                            NewPrice = product.ProductPrice - (decimal)(product.ProductPrice * PromotionDto.PromotionPercent),
                            ProductId = productId,
                            OfferId = (int)PromotionDto.OfferId
                        };

                        unitOfWork.PriceOffer.Add(newPriceOffer);
                    }
                }
                else
                {
                    var products = unitOfWork.Product.GetProducts(PromotionDto.ProductCategoryId);

                    foreach(var product in products)
                    {
                        PriceOffer newPriceOffer = new PriceOffer
                        {
                            PromoText = PromotionDto.PromotionText,
                            NewPrice = product.ProductPrice - (decimal)(product.ProductPrice * PromotionDto.PromotionPercent),
                            ProductId = product.ProductId,
                            OfferId = (int)PromotionDto.OfferId
                        };

                        unitOfWork.PriceOffer.Add(newPriceOffer);
                    }
                }

                unitOfWork.SaveChanges();
            }

            return View
                (
                      new PromotionDto
                      {
                          PromotionCategories = unitOfWork.PromotionCategory.GetPromotionCategories(),
                          ProductCategories = unitOfWork.Product.GetProductCategories()
                      }
                );
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

        public IActionResult Dashboard_Users()
        {
            return View();
        }

        public IActionResult Dashboard_Products()
        {
            return View();
        }
    }
}
