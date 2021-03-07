using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Other;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data
{
    public static class AppDBContextInit
    {
        public async static Task InitDbContext(AppDBContext appDB)
        {
            await InitCategories(appDB);
        }

        private static async Task InitCategories(AppDBContext appDB)
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

        public static async Task InitUsers(UserManager<User> userManager, IConfiguration configuration)
        {
            if (!userManager.Users.Any())
            {
                var adminSection = configuration.GetSection("AdminAccount");
                var adminEmail = adminSection["Login"];
                var password = adminSection["Password"];

                await CreateUser(adminEmail, password, IdentityRoleConstants.Admin, userManager);

                await CreateUser("user1@gmail.com", "123456", IdentityRoleConstants.User, userManager);
                await CreateUser("user2@gmail.com", "123456", IdentityRoleConstants.User, userManager);

                await CreateUser("moderator1@gmail.com", "123456", IdentityRoleConstants.Moderator, userManager);
                await CreateUser("moderator2@gmail.com", "123456", IdentityRoleConstants.Moderator, userManager);

                await CreateUser("dev1@gmail.com", "123456", IdentityRoleConstants.Dev, userManager);
                await CreateUser("dev2@gmail.com", "123456", IdentityRoleConstants.Dev, userManager);
            }
        }

        private static async Task CreateUser(string email, string password, string role, UserManager<User> userManager)
        {
            if (await userManager.FindByNameAsync(email) is null)
            {
                var user = new User 
                {
                    Email = email,
                    UserName = email 
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        public static async Task InitRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!roleManager.Roles.Any())
            {

                if (await roleManager.FindByNameAsync(IdentityRoleConstants.Admin) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(IdentityRoleConstants.Admin));
                }

                if (await roleManager.FindByNameAsync(IdentityRoleConstants.Moderator) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(IdentityRoleConstants.Moderator));
                }

                if (await roleManager.FindByNameAsync(IdentityRoleConstants.Dev) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(IdentityRoleConstants.Dev));
                }

                if (await roleManager.FindByNameAsync(IdentityRoleConstants.User) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(IdentityRoleConstants.User));
                }
            }
        }
    }
}
