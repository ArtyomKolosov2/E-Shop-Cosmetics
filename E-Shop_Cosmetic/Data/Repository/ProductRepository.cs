using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications;
using E_Shop_Cosmetic.Data.Specifications.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductsRepository
    {
        public ProductRepository(AppDBContext appDBContext) : base(appDBContext)
        {

        }

        public async Task<Product> GetProductByName(string name)
        {
            var collection = await GetAll();

            return collection.FirstOrDefault(n => n.Name.Equals(name));
        }

        public async Task<Product> GetProductByPrice(int price)
        {
            var collection = await GetAll();

            return collection.FirstOrDefault(n => n.Price == price);
        }

        public Task<IReadOnlyList<Product>> GetProductsByIds(IEnumerable<int> ids)
        {
            var specification = new ProductSpecification(prod => ids.Contains(prod.Id));
            return GetAll(specification);
        }

    }
}
