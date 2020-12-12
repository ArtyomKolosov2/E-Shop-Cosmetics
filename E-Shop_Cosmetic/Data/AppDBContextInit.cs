using E_Shop_Cosmetic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data
{
    public static class AppDBContextInit
    {
        public async static Task InitDbContextAsync(AppDBContext appDB)
        {
            await InitCategoriesAsync(appDB);
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
                new Category {CategoryName="Faberlic", Description="Косметика от faberlic"},
                new Category {CategoryName="Духи", Description="Самые приятные духи"},
                new Category {CategoryName="Краски для волос", Description="Краски"},
                new Category {CategoryName="Шампуни", Description="Шампуни для волос"},
                new Category {CategoryName="Укладка волос", Description="Для Ваших волос"},
                new Category {CategoryName="Помада", Description="Помада для Ваших губ"},
                
            };
            await appDB.Categories.AddRangeAsync(categories);
            await appDB.SaveChangesAsync();
        }

        public static async Task InitRolesAsync(AppDBContext appDB, IConfiguration configuration)
        {
            if (await appDB.Roles.AnyAsync())
            {
                return;
            }

            var categories = new Role[]
            {
                new Role {Name="user"},
                new Role {Name="admin"},
                new Role {Name="developer"}
            };
            var adminSection = configuration.GetSection("AdminAccount");
            var adminPassword = adminSection["Password"];
            var adminLogin = adminSection["Login"];
            User user = new User 
            { 
                Email = adminLogin,
                Password = adminPassword, 
                UserRoleId = 2 
            };
            await appDB.Users.AddAsync(user);
            await appDB.Roles.AddRangeAsync(categories);
            await appDB.SaveChangesAsync();
        }
    }
}
