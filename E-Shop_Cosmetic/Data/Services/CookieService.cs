using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Repository.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Services
{
    public class CookieService : ICookieCartService
    {
        private readonly IRequestCookieCollection cookieCollection;
        private readonly IProductsRepository productRepository;
        private const string CartCookieKey = "products";

        public CookieService(IHttpContextAccessor httpContextAccessor, IProductsRepository repository)
        {
            this.cookieCollection = httpContextAccessor.HttpContext.Request.Cookies;
            this.productRepository = repository;
        }

        public List<OrderDetail> GetOrderDetails()
        {
            var cartLines = GetCookieCartLines();

            return cartLines;

        }

        private List<OrderDetail> GetCookieCartLines()
        {
            var cookieCartLines = new List<OrderDetail>();
            if (cookieCollection.TryGetValue(CartCookieKey, out var cartCookieString))
            {
                cartCookieString ??= "[]";
                cookieCartLines = JsonSerializer.Deserialize<List<OrderDetail>>(cartCookieString);
            }

            return cookieCartLines;
        }
    }
}
