using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Repository;
using E_Shop_Cosmetic.Data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace E_Shop_Cosmetic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole<int>>(opts =>
            {
                opts.Password = new PasswordOptions
                {
                    RequiredLength = 6,
                    RequireNonAlphanumeric = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireDigit = false,
                };
                opts.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, IdentityRole<int>>>();
            services.AddScoped<ICategoriesRepository, CategoryRepository>();
            services.AddScoped<IProductsRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddHttpContextAccessor();
            services.AddTransient<ICookieService, CookieService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });


            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
     
            app.UseRequestLocalization();
            CultureInfo customCulture = new CultureInfo("ru-RU");
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            CultureInfo.DefaultThreadCurrentCulture = customCulture;
            CultureInfo.DefaultThreadCurrentUICulture = customCulture;

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
