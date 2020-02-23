using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Apps.SportsLeague.Data.Models
{
    public static class ProductDatabaseInitializer
    {
        public static async Task Configure(ProductContext context)
        {
            // Database Initialization
            context.Database.EnsureCreated();


            context.Database.OpenConnection();
            try
            {
                if (!await context.Categories.AnyAsync())
                {
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories ON");

                    GetCategories().ForEach(c => context.Categories.Add(c));
                    await context.SaveChangesAsync();

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories OFF");
                }

                if (!await context.Products.AnyAsync())
                {
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Products ON");

                    GetProducts().ForEach(p => context.Products.Add(p));
                    await context.SaveChangesAsync();

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Products OFF");
                }
            }
            finally
            {
                context.Database.CloseConnection();
            }


            context.SaveChanges();
        }
        private static List<Category> GetCategories()
        {
            var categories = new List<Category> {
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "Team Apparel"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Equipment"
                }
            };

            return categories;
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    ProductID = 1,
                    ProductName = "Official CSL Hat",
                    Description = "Be the envy of any jock, trucker, superfan, or hipster with the offical CSL logo hat." +
                                  "Tops off the perfect ensemble from the baseball field, to the trailer park, or your cousin's wedding. Yeehaw!",
                    ImagePath = "hat.jpg",
                    ThumbnailPath = "hat-thumb.jpg",
                    UnitPrice = 18.95,
                    CategoryID = 1
               },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Men's Tank Top",
                    Description = "When the sun's out, it's time to put your guns out... with the CSL men's tank top, of course!" +
                                  "Perfect for any occasion, such as going to the beach, the flea market, company functions, or speed dating marathons. Go get it!",
                    ImagePath = "mens_tank_top.jpg",
                    ThumbnailPath = "mens_tank_top-thumb.jpg",
                    UnitPrice = 12.98,
                    CategoryID = 1
               },
                new Product
                {
                    ProductID = 3,
                    ProductName = "Men's Hoodie",
                    Description = "Part shirt, part sweater, part sweatshirt, part hood, totally awesome. The men's hoodie is a " +
                                  "perfect companion to your monthly workout. Now, with the new and improved flip-up hood, you can go from forgettable to shady in 1.6 seconds flat!",
                    ImagePath = "hoodie.jpg",
                    ThumbnailPath = "hoodie-thumb.jpg",
                    UnitPrice = 35.98,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 4,
                    ProductName = "Baseball Socks",
                    Description = "The latest in additive garment technology knits up a surprisingly sleek pair of baseball socks, sporting " +
                                  "advanced features like, bottom 3/4 red area, topped off by alternating white-black-white racing stripes (guaranteed to make you run faster), and " +
                                  "finished off by the business-friendly formal black section on top. Betsy Ross has nothing on these vestments of virility!",
                    ImagePath = "baseball-socks.jpg",
                    ThumbnailPath = "baseball-socks-thumb.jpg",
                    UnitPrice = 16.99,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 5,
                    ProductName = "Baseball Mitt",
                    Description = "This 100% leather mitt is guaranteed to almost have been a potential candidate to be worn by baseball star Derek Jeter! " +
                                  "No batteries required.",
                    ImagePath = "baseball-mitt.jpg",
                    ThumbnailPath = "baseball-mitt-thumb.jpg",
                    UnitPrice = 45.50,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 6,
                    ProductName = "Soccer Ball",
                    Description = "Unlike most soccer balls, this one is perfectly spherical and an ideal platform on which to build your own Star Wars BB-8 droid.",
                    ImagePath = "soccerball.jpg",
                    ThumbnailPath = "soccerball-thumb.jpg",
                    UnitPrice = 28.50,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 7,
                    ProductName = "Road Bike",
                    Description = "Rub it in your hippie neighbor's nose when you ditch your gas-guzzling death machine for this sleek 100% emissions-free road bike! " +
                                  "Ask to borrow your neighbor's Prius whenever you have to do something useful, like grocery shopping, taking the dog to the vet, or anything else greater than 2 miles from your home.",
                    ImagePath = "bike.jpg",
                    ThumbnailPath = "bike-thumb.jpg",
                    UnitPrice = 1250.99,
                    CategoryID = 2
                }
            };

            return products;
        }
    }
}