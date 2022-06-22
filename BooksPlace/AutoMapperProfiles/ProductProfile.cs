using AutoMapper;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Data.Repository.UnitOfWork;
using BooksPlace.ExtensionMethods;
using BooksPlace.Models;
using BooksPlace.Models.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.PromoText, opt => opt.MapFrom(src => src.PriceOffer.PromoText))
                .ForMember(dest => dest.NewPrice, opt => opt.MapFrom(src => src.PriceOffer.NewPrice));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.ProductImage, opt =>
                 opt.MapFrom(src=> src.PictureFile.GetBytes().Result));
        }
    }
}