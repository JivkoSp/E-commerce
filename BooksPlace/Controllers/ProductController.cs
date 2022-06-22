using AutoMapper;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.MessageBroker;
using BooksPlace.Models;
using BooksPlace.Models.Dtos;
using BooksPlace.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksPlace.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private IUnitOfWork unitOfWork;
        private UserManager<User> userManager;
        private RabbitMqHub mqHub;
        private IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork, UserManager<User> userManager, 
            RabbitMqHub mqHub, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mqHub = mqHub;
            this.mapper = mapper;
        }

        public IActionResult ProductView(int productId)
        {
            return View
                (
                    new ProductInformationViewModel
                    {
                        ProductDto = mapper.Map<ProductDto>(unitOfWork.Product.GetProduct(productId)),
                        Reviews = unitOfWork.Review.GetReviews(productId)
                    }
                );
        }
        
        
        [HttpPost]
        public async Task<IActionResult> PostReview(IFormFile PictureFile, string ReviewContent, int ProductId)
        {         
            string userId = userManager.GetUserId(User);

            if (!unitOfWork.Review.isUserExising(userId, ProductId))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await PictureFile.CopyToAsync(memoryStream);

                    Review review = new Review
                    {
                        ReviewContent = ReviewContent,
                        ReviewImage = memoryStream.ToArray(),
                        DateTime = DateTime.Now,
                        UserId = userId,
                        ProductId = ProductId,
                        User = await userManager.FindByIdAsync(userId),
                        Product = unitOfWork.Product.Get(ProductId)
                    };

                    unitOfWork.Review.Add(review);
                    unitOfWork.SaveChanges();
                }
            }

            return RedirectToAction("ProductView", new { productId = ProductId });
        }


        [HttpPost]
        public async Task PostComment(string comment, int reviewId)
        {
            string userId = userManager.GetUserId(User);
            var user = await userManager.FindByIdAsync(userId);
            var review = unitOfWork.Review.GetReview(reviewId);

            if(!unitOfWork.ReviewComment.isUserExising(userId, reviewId))
            {
                ReviewComment reviewComment = new ReviewComment
                {
                    Comment = comment,
                    DateTime = DateTime.Now,
                    Likes = 0,
                    ReviewId = review.ReviewId,
                    Review = review,
                    UserId = userId,
                    User = user
                };

                mqHub.Publish(reviewComment);
                unitOfWork.ReviewComment.Add(reviewComment);
                unitOfWork.SaveChanges();
            }
        }


        [HttpGet]
        public JsonResult GetComment()
        {
            return new JsonResult(mqHub.Pull(User.Identity.Name));
        }
    }
}
