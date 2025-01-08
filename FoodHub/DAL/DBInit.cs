using Microsoft.EntityFrameworkCore;
using FoodHub.Models;

namespace FoodHub.DAL;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ItemDbContext context = serviceScope.ServiceProvider.GetRequiredService<ItemDbContext>();
        context.Database.EnsureDeleted();
        //Using Migrate() instead of EnsureCreated() since Migration is being used in this project
        context.Database.Migrate();

        if (!context.ItemCategories.Any())
        {
            var itemcategories = new List<ItemCategory>
            {
                //Categories mostly taken from the national norwegian food groups within "Nøkkelhullforskriften", excluding the smaller categories
                new ItemCategory { ItemCategoryId = 1, Name = "Dairy products"},      
                new ItemCategory { ItemCategoryId = 2, Name = "Egg"},        
                new ItemCategory { ItemCategoryId = 3, Name = "Meat and poultry"},
                new ItemCategory { ItemCategoryId = 4, Name = "Fish and shellfish"},
                new ItemCategory { ItemCategoryId = 5, Name = "Cereals, bread and cakes"},
                new ItemCategory { ItemCategoryId = 6, Name = "Vegetables"},
                new ItemCategory { ItemCategoryId = 7, Name = "Beverages"},
                new ItemCategory { ItemCategoryId = 8, Name = "Legumes"},
                new ItemCategory { ItemCategoryId = 9, Name = "Fruit and berries"},
                new ItemCategory { ItemCategoryId = 10, Name = "Nuts and seeds"},
                new ItemCategory { ItemCategoryId = 11, Name = "Potatoes"},
                new ItemCategory { ItemCategoryId = 12, Name = "Herbs and spices"},
                new ItemCategory { ItemCategoryId = 13, Name = "Sugar and sweet products"},
                new ItemCategory { ItemCategoryId = 14, Name = "Cooking fat"},
                new ItemCategory { ItemCategoryId = 15, Name = "Other foods and dishes"}
            };
            context.AddRange(itemcategories);
            context.SaveChanges();
        }

        //Temp variables for seeded database entries
        var milkCategory = context.ItemCategories.First(c => c.ItemCategoryId == 1);
        var eggCategory = context.ItemCategories.First(c => c.ItemCategoryId == 2);
        var drinkCategory = context.ItemCategories.First(c => c.ItemCategoryId == 7);
        var potatoCategory = context.ItemCategories.First(c => c.ItemCategoryId == 11);
        var oilCategory = context.ItemCategories.First(c => c.ItemCategoryId == 14);
        var fastfoodCategory = context.ItemCategories.First(c => c.ItemCategoryId == 15);
        
        if (!context.Allergens.Any())
        {
            var allergens = new List<Allergen>
            {
                new Allergen { AllergenId = 1, Name = "Celery" },
                new Allergen { AllergenId = 2, Name = "Egg" },
                new Allergen { AllergenId = 3, Name = "Fish" },
                new Allergen { AllergenId = 4, Name = "Gluten" },
                new Allergen { AllergenId = 5, Name = "Milk" },
                new Allergen { AllergenId = 6, Name = "Mustard" },
                new Allergen { AllergenId = 7, Name = "Nuts" },
                new Allergen { AllergenId = 8, Name = "Peanuts" },
                new Allergen { AllergenId = 9, Name = "Lupins" },
                new Allergen { AllergenId = 10, Name = "Mollusks" },
                new Allergen { AllergenId = 11, Name = "Sesame" },
                new Allergen { AllergenId = 12, Name = "Crustacean" },
                new Allergen { AllergenId = 13, Name = "Soy" },
                new Allergen { AllergenId = 14, Name = "Sulfites" },
            };
            context.AddRange(allergens);
            context.SaveChanges();
        }

        //Temp variables for allergens in seeded database entries
        var eggAllergen = context.Allergens.First(a => a.AllergenId == 2);
        var glutenAllergen = context.Allergens.First(a => a.AllergenId == 4);
        var milkAllergen = context.Allergens.First(a => a.AllergenId == 5);
        var soyAllergen = context.Allergens.First(a => a.AllergenId == 13);
        
        if (!context.Items.Any())
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Pizza Grandiosa Original",
                    ProducerName = "Orkla",
                    //Source: https://www.grandiosa.no/wp-content/uploads/sites/80/2024/12/1980.png
                    //Alt Source: https://www.grandiosa.no/produkter/grandiosa-original/    
                    ImagePath = "/images/grandiosa-original.png",
                    Description = "Grandiosa Original has a filling of tomato sauce, pizza meat and paprika. It is topped with Jarlsberg®, which gives the pizza a taste of its own. The original Grandiosa was launched in 1980 and since then has been by far Norway's most eaten pizza. ",
                    Energy = 218M,
                    Carbohydrate = 26M,
                    TotalFat = 7.7M,
                    SaturatedFat = 4.2M,
                    UnsaturatedFat = 3.5M,
                    Sugar = 2.8M,
                    DietaryFiber = 3M,
                    Protein = 11M,
                    Salt = 0.9M,
                    ItemCategoryId = fastfoodCategory.ItemCategoryId,
                    ItemCategory = fastfoodCategory,
                    ItemAllergen = new List<ItemAllergen>
                    {
                        new ItemAllergen { AllergenId = glutenAllergen.AllergenId },
                        new ItemAllergen { AllergenId = milkAllergen.AllergenId },
                        new ItemAllergen { AllergenId = soyAllergen.AllergenId },
                    }
                },
                new Item
                {
                    Name = "Kylling Tikka 480g, Fersk & Ferdig",
                    ProducerName = "Unil",
                    //Source: https://bilder.ngdata.no/7035620052279/customerfacing/large.jpg?fallback=404
                    ImagePath = "/images/fersk-ferdig-tikka.jpg",
                    Description = "Ferdig Kylling Tikka inspirert av det indiske kjøkken. Kyllingbiter i deilig masala-saus med basmatiris. Server sammen med naan-brød og chutney. Rask og enkel middag. Varmes i micro.",
                    Energy = 138M,
                    Carbohydrate = 13M,
                    TotalFat = 5.2M,
                    SaturatedFat = 1.5M,
                    UnsaturatedFat = 3.7M,
                    Sugar = 2.6M,
                    DietaryFiber = 1.3M,
                    Protein = 9.3M,
                    Salt = 0.5M,
                    ItemCategoryId = fastfoodCategory.ItemCategoryId,
                    ItemCategory = fastfoodCategory,
                    ItemAllergen = new List<ItemAllergen>
                    {
                        new ItemAllergen { AllergenId = milkAllergen.AllergenId },
                    }
                },
                new Item
                {
                    Name = "Opphøgde Potteter - Originalen",
                    ProducerName = "HOFF",
                    //Source: https://www.hoff.no/wp-content/webp-express/webp-images/uploads/2024/03/HOFF-Opphogde-Potteter-Originalen_Familiepose-mockup_136-100-kopi-412x303.png.webp
                    ImagePath = "/images/potteter.webp",
                    Description = "HOFF Opphøgde Potteter Originalen har blitt laget på fabrikken i Gjøvik i flere generasjoner. Med sin sprøe overflate og myke innside har den blitt en av nordmenns favoritter! Produktet stekes ferdig på bare 9 minutter i ovnen.",
                    Energy = 198M,
                    Carbohydrate = 34M,
                    TotalFat = 6M,
                    SaturatedFat = 0.7M,
                    UnsaturatedFat = 5.3M,
                    Sugar = 1.3M,
                    DietaryFiber = 0M,
                    Protein = 2M,
                    Salt = 0M,
                    ItemCategoryId = potatoCategory.ItemCategoryId,
                    ItemCategory = potatoCategory,
                },
                new Item
                {
                    Name = "Egg Frittgående, 12stk",
                    ProducerName = "Prior",
                    //Source: https://bilder.ngdata.no/7039610000172/meny/medium.jpg
                    ImagePath = "/images/egg-prior.jpg",
                    Description = "Egg fra norske bønder hvor hønene har tilgang på elementer som gir økt trivsel.",
                    Energy = 149M,
                    Carbohydrate = 0.3M,
                    TotalFat = 11M,
                    SaturatedFat = 3M,
                    UnsaturatedFat = 8M,
                    Sugar = 0.3M,
                    DietaryFiber = 0M,
                    Protein = 13M,
                    Salt = 0.4M,
                    ItemCategoryId = eggCategory.ItemCategoryId,
                    ItemCategory = eggCategory,
                    ItemAllergen = new List<ItemAllergen>
                    {
                        new ItemAllergen { AllergenId = eggAllergen.AllergenId },
                    }
                },
                new Item
                {
                    Name = "YT Proteinmelk Kakao, 1L",
                    ProducerName = "Tine",
                    //Source: https://bilder.ngdata.no/7038010068157/meny/medium.jpg
                    ImagePath = "/images/yt-proteinmelk-kakao-1l.jpg",
                    Description = "YT® Proteinmelk Kakao er sunn kakaomelk med 68 % mer protein enn annen sjokolademelk. I tillegg så er den fettfri, laktoseredusert og uten tilsatt sukker, med naturlig sukker fra melka",
                    Energy = 45M,
                    Carbohydrate = 4.7M,
                    TotalFat = 0.3M,
                    SaturatedFat = 0.2M,
                    UnsaturatedFat = 0.1M,
                    Sugar = 4.5M,
                    DietaryFiber = 0M,
                    Protein = 6M,
                    Salt = 0.1M,
                    ItemCategoryId = milkCategory.ItemCategoryId,
                    ItemCategory = milkCategory,
                    ItemAllergen = new List<ItemAllergen>
                    {
                        new ItemAllergen { AllergenId = milkAllergen.AllergenId },
                    }
                },
                new Item
                {
                    Name = "Olivenolje, 500ml",
                    ProducerName ="Eldorado",
                    //Source: https://bilder.ngdata.no/7311041001400/meny/medium.jpg
                    ImagePath = "/images/olivenolje-eldorado.jpg",
                    Description = "En klassisk, spansk olivenolje med en rund og mild smak. Passer godt til pasta, steking og baking.",
                    Energy = 900M,
                    Carbohydrate = 60M,
                    TotalFat = 100M,
                    SaturatedFat = 14M,
                    UnsaturatedFat = 86M,
                    Sugar = 0M,
                    DietaryFiber = 0M,
                    Protein = 0M,
                    Salt = 0M,
                    ItemCategoryId = oilCategory.ItemCategoryId,
                    ItemCategory = oilCategory
                },
                new Item
                {
                    Name = "Coca-Cola Uten Sukker",
                    ProducerName = "Coca-Cola Europacific Partners Norge",
                    //Source: https://www.coca-cola.com/content/dam/onexp/no/no/home-images/brands/coca-cola/no_cocacola_without_sugar_750X750.jpg/width1024.jpg
                    ImagePath = "/images/coca-cola-zero-sugar.webp",
                    Description = "Coca-Cola® Uten Sukker ble lansert i 2005, under navnet Coca-Cola® zero. Coca-Cola® Zero Sugar ligger så tett opp til originalen som mulig, og er en del av strategien for å bidra til å redusere folks sukkerinntak.",
                    Energy = 0.3M,
                    Carbohydrate = 0M,
                    TotalFat = 0M,
                    SaturatedFat = 0M,
                    UnsaturatedFat = 0M,
                    Sugar = 0M,
                    DietaryFiber = 0M,
                    Protein = 0M,
                    Salt = 0M,
                    ItemCategoryId = drinkCategory.ItemCategoryId,
                    ItemCategory = drinkCategory
                },
            };
            context.AddRange(items);
            context.SaveChanges();
        }
    }
}