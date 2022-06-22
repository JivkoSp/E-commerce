﻿using BooksPlace.Data.Repository.GenericRepo;
using BooksPlace.Data.Repository.Interfaces;
using BooksPlace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.Repository
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        private BooksPlaceDbContext BooksPlaceDbContext => dbContext as BooksPlaceDbContext;

        public ProductRepo(BooksPlaceDbContext dbContext):base(dbContext)
        {
        }
        
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return BooksPlaceDbContext.Products
                .Select(p => p.ProductCategory)
                .Distinct()
                .OrderBy(p => p.Name);
        }

        public IEnumerable<Product> GetViewProducts(string productCategory, int pageNumber, int pageSize)
        {
            return BooksPlaceDbContext.Products.Where(p => productCategory == null ||
                    p.ProductCategory.Name == productCategory)
                    .Include(p => p.PriceOffer)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(p => p.ProductName);
        }

        public IEnumerable<Product> GetProducts(string productCategory)
        {
            IEnumerable<Product> products;
            if (productCategory != null)
            {
                products = BooksPlaceDbContext.Products.Where(p => p.ProductCategory.Name == productCategory);
            }
            else { products = BooksPlaceDbContext.Products; }

            return products;
        }

        public Product GetProduct(int id)
        {
            return BooksPlaceDbContext.Products.Where(p => p.ProductId == id)
                .Include(p => p.PriceOffer)
                .Include(p => p.ProductCategory).FirstOrDefault();
        }

        public List<string> SearchProductNames(string searchTerm)
        {
            return BooksPlaceDbContext.Products.Where(p => p.ProductName.Contains(searchTerm))
                    .Select(p => p.ProductName).ToList();
        }

        public int GetProductId(string productName)
        {
            return BooksPlaceDbContext.Products.Where(p => p.ProductName == productName)
                    .Select(p => p.ProductId).FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts(int productCategoryId)
        {
            return BooksPlaceDbContext.Products.Where(p => p.ProductCategoryId == productCategoryId);
        }
    }
}
