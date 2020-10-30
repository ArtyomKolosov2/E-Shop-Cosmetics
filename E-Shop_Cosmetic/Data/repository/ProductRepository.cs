using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository
{
    public class ProductRepository : IProducts
    {
        private readonly AppDBContext appDBContent;

        public ProductRepository(AppDBContext appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Product> GetProducts => appDBContent.Products.Include(c => c.Category);

        public IEnumerable<Product> GetFavoriteProducts => appDBContent.Products.Where(c => c.IsFavorite).Include(p => p.Category);

        public Product GetProductById(int productId)
        {

            return appDBContent.Products.FirstOrDefault(p => p.id == productId);
        }
    }
}
