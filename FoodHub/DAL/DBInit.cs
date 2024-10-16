using Microsoft.EntityFrameworkCore;
using FoodHub.Models;

namespace FoodHub.DAL;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ItemDbContext context = serviceScope.ServiceProvider.GetRequiredService<ItemDbContext>();
        // context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (!context.Items.Any())
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Pizza",
                    ImageUrl = "/images/pizza.jpg",
                    Description = "Delicious Italian dish with a thin crust topped with tomato sauce, cheese, and various toppings.",
                    Energy = 250M,
                    Carbohydrate = 30M,
                    TotalFat = 10M,
                    SaturatedFat = 4M,
                    UnsaturedFat = 6M,
                    Sugar = 2M,
                    DietaryFiber = 3M,
                    Protein = 12M,
                    Salt = 1M
                },
                new Item
                {
                    Name = "Fried Chicken Leg",
                    ImageUrl = "/images/chickenleg.jpg",
                    Description = "Crispy and succulent chicken leg that is deep-fried to perfection, often served as a popular fast food item.",
                    Energy = 300M,
                    Carbohydrate = 15M,
                    TotalFat = 20M,
                    SaturatedFat = 6M,
                    UnsaturedFat = 10M,
                    Sugar = 0M,
                    DietaryFiber = 0M,
                    Protein = 25M,
                    Salt = 1.5M
                },
                new Item
                {
                    Name = "French Fries",
                    ImageUrl = "/images/frenchfries.jpg",
                    Description = "Crispy, golden-brown potato slices seasoned with salt and often served as a popular side dish or snack.",
                    Energy = 365M,
                    Carbohydrate = 63M,
                    TotalFat = 17M,
                    SaturatedFat = 2M,
                    UnsaturedFat = 15M,
                    Sugar = 0M,
                    DietaryFiber = 4M,
                    Protein = 4M,
                    Salt = 0.6M
                },
                new Item
                {
                    Name = "Grilled Ribs",
                    ImageUrl = "/images/ribs.jpg",
                    Description = "Tender and flavorful ribs grilled to perfection, usually served with barbecue sauce.",
                    Energy = 400M,
                    Carbohydrate = 10M,
                    TotalFat = 30M,
                    SaturatedFat = 12M,
                    UnsaturedFat = 15M,
                    Sugar = 5M,
                    DietaryFiber = 0M,
                    Protein = 25M,
                    Salt = 2M
                },
                new Item
                {
                    Name = "Tacos",
                    ImageUrl = "/images/tacos.jpg",
                    Description = "Tortillas filled with various ingredients such as seasoned meat, vegetables, and salsa, folded into a delicious handheld meal.",
                    Energy = 200M,
                    Carbohydrate = 30M,
                    TotalFat = 8M,
                    SaturatedFat = 3M,
                    UnsaturedFat = 5M,
                    Sugar = 1M,
                    DietaryFiber = 3M,
                    Protein = 10M,
                    Salt = 0.5M
                },
                new Item
                {
                    Name = "Fish and Chips",
                    ImageUrl = "/images/fishandchips.jpg",
                    Description = "Classic British dish featuring battered and deep-fried fish served with thick-cut fried potatoes.",
                    Energy = 550M,
                    Carbohydrate = 60M,
                    TotalFat = 30M,
                    SaturatedFat = 7M,
                    UnsaturedFat = 20M,
                    Sugar = 0M,
                    DietaryFiber = 3M,
                    Protein = 20M,
                    Salt = 1M
                },
                new Item
                {
                    Name = "Cider",
                    ImageUrl = "/images/cider.jpg",
                    Description = "Refreshing alcoholic beverage made from fermented apple juice, available in various flavors.",
                    Energy = 50M,
                    Carbohydrate = 12M,
                    TotalFat = 0M,
                    SaturatedFat = 0M,
                    UnsaturedFat = 0M,
                    Sugar = 10M,
                    DietaryFiber = 0M,
                    Protein = 0M,
                    Salt = 0M
                },
                new Item
                {
                    Name = "Coke",
                    ImageUrl = "/images/coke.jpg",
                    Description = "Popular carbonated soft drink known for its sweet and refreshing taste.",
                    Energy = 140M,
                    Carbohydrate = 39M,
                    TotalFat = 0M,
                    SaturatedFat = 0M,
                    UnsaturedFat = 0M,
                    Sugar = 39M,
                    DietaryFiber = 0M,
                    Protein = 0M,
                    Salt = 0.02M
                },
            };
            context.AddRange(items);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Alice Hansen", Address = "Osloveien 1"},
                new Customer { Name = "Bob Johansen", Address = "Oslomet gata 2"},
            };
            context.AddRange(customers);
            context.SaveChanges();
        }
    }
}
