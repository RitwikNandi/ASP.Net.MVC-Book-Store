using Book_Store_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Book_Store_DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Test",
                    DisplayOrder = 1,
                });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Description = "A classic novel about racial injustice in the American South.",
                    Author = "Harper Lee",
                    ISBN = "9780446310775",
                    ListPrice = 50,
                    List50 = 90,
                    Price100 = 70,
                    ImageUrl ="",
                    CategoryId = 1
                },
                new Product {
                    Id = 1,
                    Title = "1984",
                    Description = "A dystopian novel about a totalitarian government.",
                    Author = "George Orwell",
                    ISBN = "9780451534856",
                    ListPrice = 50,
                    List50 = 90,
                    Price100 = 70,
                    ImageUrl = "",
                    CategoryId = 1
                }
                );
        }
    }
}
