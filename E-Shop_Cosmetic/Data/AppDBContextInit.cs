using E_Shop_Cosmetic.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data
{
    public static class AppDBContextInit
    {
        public async static Task InitDbContextAsync(AppDBContext appDB)
        {
            await InitCategoriesAsync(appDB);
            await InitRolesAsync(appDB);
        }
        private static async Task InitCategoriesAsync(AppDBContext appDB)
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

        private static async Task InitRolesAsync(AppDBContext appDB)
        {
            if (await appDB.Roles.AnyAsync())
            {
                return;
            }

            var categories = new Role[]
            {
                new Role {Name="user"},
                new Role {Name="admin"}
            };
            User user = new User { Email = "artyomkolosov2@yandex.ru", Password="228228", UserRoleId = 2 };
            await appDB.Users.AddAsync(user);
            await appDB.Roles.AddRangeAsync(categories);
            await appDB.SaveChangesAsync();
        }
    }
}
