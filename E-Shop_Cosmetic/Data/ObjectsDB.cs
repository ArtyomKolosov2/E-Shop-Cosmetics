using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace E_Shop_Cosmetic.Data
{
    public static class ObjectsDB
    {
        public static void Initial(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent dBContent = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                if (!dBContent.Categories.Any())
                {
                    dBContent.Categories.AddRange(AppCategories.Select(content => content.Value));
                }

                if (!dBContent.Products.Any())
                {
                    dBContent.AddRange
                    (
                        new Product
                        {
                            Name = "Chi laxury",
                            ShortDescription = "",
                            LongDescription = "",
                            ImageURL = "https://www.cosmoprofbeauty.com/on/demandware.static/-/Sites-cosmoprof-master-catalog/default/dw375c8a15/images/farouk/639204.png",
                            Price = 55,
                            IsFavorite = true,
                            IsAvailable = true,
                            Category = AppCategories["CHI"]
                        },
                        new Product
                        {
                            Name = "Chi IronGuard",
                            ShortDescription = "",
                            LongDescription = "",
                            ImageURL = "https://chi.com/wp-content/uploads/2014/12/CHI-Iron-Guard-44-Thermal-Protecting-Shampoo-12floz-New2-800x800.jpg",
                            Price = 45,
                            IsFavorite = true,
                            IsAvailable = true,
                            Category = AppCategories["CHI"]
                        },
                        new Product
                        {
                            Name = "Faberlic Home",
                            ShortDescription = "",
                            LongDescription = "",
                            ImageURL = "https://fbrlc.by/wp-content/uploads/2020/05/stirka_0_small_Stranica_10-1536x1220.jpg",
                            Price = 25,
                            IsFavorite = false,
                            IsAvailable = true,
                            Category = AppCategories["Faberlic"]
                        }
                    );
                }
                dBContent.SaveChanges();
            }

        }
        private static Dictionary<string, Category> _categories;
        public static Dictionary<string, Category> AppCategories
        {
            get
            {
                if (_categories == null)
                {
                    var list = new Category[]
                    {
                        new Category {CategoryName="CHI", Description="Косметика от CHI"},
                        new Category {CategoryName="Faberlic", Description="Косметика от faberlic"}
                    };
                    _categories = new Dictionary<string, Category>();
                    foreach (Category element in list)
                    {
                        _categories.Add(element.CategoryName, element);
                    }
                }
                return _categories;
            }
        }
    }
}

