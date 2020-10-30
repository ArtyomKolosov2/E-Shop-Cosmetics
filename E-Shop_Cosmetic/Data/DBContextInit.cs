using E_Shop_Cosmetic.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data
{
    public static class DBContextInit
    {
        public async static Task InitDbContextAsync(AppDBContent appDB)
        {
            await InitCategoriesAsync(appDB);
        }
        private static async Task InitCategoriesAsync(AppDBContent appDB)
        {
            if (await appDB.Categories.AnyAsync())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category {CategoryName="CHI", Description="Косметика от CHI"},
                new Category {CategoryName="Faberlic", Description="Косметика от faberlic"}
            };
            await appDB.Categories.AddRangeAsync(categories);
            await appDB.SaveChangesAsync();
        }
    }
}
