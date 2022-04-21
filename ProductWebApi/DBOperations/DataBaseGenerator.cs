using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductWebApi.Entities;
using System;
using System.Linq;

namespace ProductWebApi.DBOperations
{
    public class DataBaseGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<ProductStoreDbContext>>()))
            {
                if (context.Products.Any())
                {
                    return;
                }

                context.Products.AddRange(new Product()
                {
                    Name = "Bol Fit Kadın Pantolon",
                    CategoryId = 1,
                    BrandId = 1,
                    Price = 150,
                    Size = "Small",
                    Stock = 85,
                },
                new Product()
                {
                    Name = "Standart Kalıp Erkek Ceket",
                    CategoryId = 3,
                    BrandId = 3,
                    Price = 300,
                    Size = "Medium",
                    Stock = 120,
                },
                new Product()
                {
                    Name = "Kısa Kollu Kadın Elbise",
                    CategoryId = 7,
                    BrandId = 4,
                    Price = 280,
                    Size = "Large",
                    Stock = 30,
                });


                if (context.Categories.Any())
                {
                    return;
                }

                context.Categories.AddRange(new Category()
                {
                    CategoryId = 1,
                    Name = "Pantolon",
                    Style = "Fit",
                },
                new Category()
                {
                    CategoryId = 2,
                    Name = "Gömlek",
                    Style = "Bol",
                },
                new Category()
                {
                    CategoryId = 3,
                    Name = "Ceket",
                    Style = "Standart",
                },
                new Category()
                {
                    CategoryId = 4,
                    Name = "Mont",
                    Style = "Uzun",
                },
                new Category()
                {
                    CategoryId = 5,
                    Name = "Yelek",
                    Style = "Dar",
                });

                context.SaveChanges();
            }
        }
    }
}
