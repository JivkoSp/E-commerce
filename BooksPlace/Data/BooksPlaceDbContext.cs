using BooksPlace.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data
{
    public class BooksPlaceDbContext : IdentityDbContext<User>
    {
        public BooksPlaceDbContext(DbContextOptions<BooksPlaceDbContext> options):base(options)
        {
        }

        public virtual DbSet<PromotionCategory> PromotionCategories { get; set; }
        public virtual DbSet<User> _Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<PriceOffer> PriceOffers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ReviewComment> ReviewComments { get; set; }
        public virtual DbSet<BannedUser> BannedUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<PromotionCategory>(entity => {

                entity.ToTable("PromotionCategory");
                
                entity.Property(p => p.Name)
                .HasMaxLength(20)
                .IsRequired();
            });

            modelBuilder.Entity<User>(entity => {

                entity.ToTable("User");

                entity.HasOne(p => p.PromotionCategory)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.PromotionCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_PromotionCategory");
            });

            modelBuilder.Entity<Order>(entity => {

                entity.ToTable("Order");

                entity.Property(p => p.AddressLine).IsRequired();
                entity.Property(p => p.City).IsRequired();
                entity.Property(p => p.Country).IsRequired();
                entity.Property(p => p.Zip).IsRequired();
                entity.Property(p => p.DateTime)
                    .HasColumnType("datetime2");

                entity.HasOne(p => p.User)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<ProductCategory>(entity => {

                entity.ToTable("ProductCategory");

                entity.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            });

            modelBuilder.Entity<Product>(entity => {

                entity.ToTable("Product");

                entity.Property(p => p.ProductName)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(p => p.ProductDescription)
                .HasMaxLength(100)
                .IsRequired();

                entity.Property(p => p.ProductPrice)
                .HasColumnType("decimal(8, 2)")
                .IsRequired();

                entity.Property(p => p.ProductImage)
                .HasColumnType("image");

                entity.HasOne(p => p.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Product_ProductCategory");
            });

            modelBuilder.Entity<ProductOrder>(entity => {

                entity.ToTable("ProductOrder");
                entity.HasKey(p => new { p.ProductId, p.OrderId});

                entity.Property(p => p.Quantity)
                .HasColumnType("decimal(8, 2)")
                .IsRequired();

                entity.HasOne(p => p.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductOrder_Product");

                 entity.HasOne(p => p.Order)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductOrder_Order");
            });


            modelBuilder.Entity<Offer>(entity => {

                entity.ToTable("Offer");

                entity.Property(p => p.OfferPercent)
                .HasColumnType("decimal(8, 2)")
                .IsRequired();

                entity.HasOne(p => p.PromotionCategory)
                .WithOne(p => p.Offer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Offer_PromotionCategory");
            });

            modelBuilder.Entity<PriceOffer>(entity => {

                entity.ToTable("PriceOffer");

                entity.Property(p => p.PromoText)
                .HasMaxLength(50);

                entity.Property(p => p.NewPrice).HasColumnType("decimal(8, 2)");

                entity.HasOne(p => p.Offer)
                .WithMany(p => p.PriceOffers)
                .HasForeignKey(p => p.OfferId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PriceOffer_Offer");

                entity.HasOne(p => p.Product)
                .WithOne(p => p.PriceOffer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PriceOffer_Product");
            });

            modelBuilder.Entity<Review>(entity => {

                entity.ToTable("Review");

                entity.Property(p => p.ReviewImage)
                .HasColumnType("image");

                entity.Property(p => p.DateTime)
                .HasColumnType("datetime2");

                entity.HasOne(p => p.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Review_Product");

                entity.HasOne(p => p.User)
                .WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Review_User");
            });

            modelBuilder.Entity<ReviewComment>(entity => {

                entity.ToTable("ReviewComment");

                entity.Property(p => p.DateTime)
                .HasColumnType("datetime2");

                entity.HasOne(p => p.User)
                .WithMany(p => p.ReviewComments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_ReviewComment_User");

                entity.HasOne(p => p.Review)
                .WithMany(p => p.ReviewComments)
                .HasForeignKey(p => p.ReviewId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ReviewComment_Review");

                entity.HasOne(p => p.ParentComment)
                .WithMany(p => p.ReviewComments)
                .HasForeignKey(p => p.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReviewComment_ReviewComment");
            });

            modelBuilder.Entity<BannedUser>(entity => {

                entity.ToTable("BannedUser");

                entity.Property(p => p.BannDate)
                .HasColumnType("datetime2");

                entity.HasOne(p => p.User)
                .WithOne(p => p.BannedUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BannedUser_User");
            });
        }
    }
}
