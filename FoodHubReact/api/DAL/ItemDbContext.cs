using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodHub.Models;

namespace FoodHub.DAL
{
    // DbContext class for the FoodHub application, inheriting from IdentityDbContext for identity management
    public class ItemDbContext : IdentityDbContext
    {
        // Constructor accepting DbContextOptions and passing them to the base class constructor
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {
        }

        // DbSet properties representing tables in the database
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<ItemAllergen> ItemAllergens { get; set; }

        // Configuring the DbContext to use lazy loading proxies
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}