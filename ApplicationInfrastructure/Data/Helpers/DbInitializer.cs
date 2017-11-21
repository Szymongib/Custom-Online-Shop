using ApplicationCore.Models;
using ApplicationInfrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;

namespace ApplicationInfrastructure.Data.Helpers
{
    public static class DbInitializer
    {
        public static void Initialize(OnlineShopDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            var categories = new ProductCategory[]
            {
                new ProductCategory{ Category="ExampleCategory1"},
                new ProductCategory{Category="ExampleCategory2"},
                new ProductCategory{Category="ExampleCategory3"}
            };
            foreach (var item in categories)
            {
                context.ProductCategories.Add(item);
            }
            context.SaveChanges();

            var products = new Product[]
            {
                new Product{ CategoryId = 1,
                    Name ="Example Product 1",
                    Price =10.5M,
                    ShortDescription ="Short Description of product 1",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit.",
                    ImgPath="images/products/example_image_1.jpg"
                },
                new Product{ CategoryId = 2,
                    Name ="Example Product 2", Price=15.05M,
                    ShortDescription="Short Description of product 2",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit.",
                    ImgPath="images/products/example_image_2.jpg"
                },
                new Product{ CategoryId = 1,
                    Name ="Example Product 3",
                    Price =115.05M,
                    ShortDescription="Short Description of product 3",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 3,
                    Name ="Example Product 4",
                    Price =3565.05M,
                    ShortDescription="Short Description of product 4",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 3,
                    Name ="Example Product 5",
                    Price =3565.05M,
                    ShortDescription="Short Description of product 5",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 2,
                    Name ="Example Product 6",
                    Price =3565.05M,
                    ShortDescription="Short Description of product 4",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 3,
                    Name ="Example Product 7",
                    Price =3565.05M,
                    ShortDescription="Short Description of product 7",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 1,
                    Name ="Example Product 8",
                    Price =356M,
                    ShortDescription="Short Description of product 8",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 3,
                    Name ="Example Product 9",
                    Price =3565.05M,
                    ShortDescription="Short Description of product 9",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 3,
                    Name ="Example Product 10",
                    Price =3565.05M,
                    ShortDescription="Short Description of product 10",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 3,
                    Name ="Example Product 11",
                    Price =120.19M,
                    ShortDescription="Short Description of product 11",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 3,
                    Name ="Example Product 12",
                    Price =3565.05M,
                    ShortDescription="Short Description of product 12",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                },
                new Product{ CategoryId = 3,
                    Name ="Example Product 13",
                    Price =3565.05M,
                    ShortDescription="Short Description of product 13",
                    FullDescription ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc rhoncus ipsum velit."
                }

            };
            foreach (var item in products)
            {
                context.Products.Add(item);
            }
            context.SaveChanges();
          
        }

    }
}
