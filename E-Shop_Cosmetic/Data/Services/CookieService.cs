using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Repository.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Services
{
    public class CookieService : ICookieCartService
    {
        private readonly IRequestCookieCollection cookieCollection;
        private readonly IProductsRepository productRepository;
        private const string CartCookieKey = "products";

        private class Detail
        {
            public int id { get; set; }
            public int number { get; set; }
            public string name { get; set; }
            public string cost { get; set; }
        }
        public CookieService(IHttpContextAccessor httpContextAccessor, IProductsRepository repository)
        {
            this.cookieCollection = httpContextAccessor.HttpContext.Request.Cookies;
            this.productRepository = repository;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync()
        {
            var cartLines = GetCookieCartLines();
            var products = await productRepository.GetProductsByIdsAsync(cartLines.Select(p => p.id));
            var orderDetailsList = products.Zip(cartLines,
                (product, line) => new OrderDetail { Amount = (uint)line.number, ProductId = product.Id }).ToList();
            return orderDetailsList;

        }

        private List<Detail> GetCookieCartLines()
        {
            var cookieCartLines = new List<Detail>();
            if (cookieCollection.TryGetValue(CartCookieKey, out var cartCookieString))
            {
                cartCookieString ??= "[]";
                cookieCartLines = JsonConvert.DeserializeObject<List<Detail>>(cartCookieString);
            }

            return cookieCartLines;
        }
    }
}
