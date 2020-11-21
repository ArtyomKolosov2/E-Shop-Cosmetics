using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
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

        public async Task AddProduct(Product product)
        {
            await AddAsync(product);
        }

        public async Task<IReadOnlyList<Product>> GetFavoriteProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await GetByIdAsync(productId);
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var collection = await GetAllAsync();
            return collection.FirstOrDefault(n => n.Name.Equals(name));
        }

        public async Task<Product> GetProductByPriceAsync(int price)
        {
            var collection = await GetAllAsync();
            return collection.FirstOrDefault(n => n.Price == price);
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            return await GetAllAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProducts(ISpecification<Product> specification)
        {
            return await GetAllAsync(specification);
        }

        public async Task<IReadOnlyList<Product>> GetProductListAsync(ISpecification<Product> specification)
        {
            return await GetAllAsync(specification);
        }

        public Task<IReadOnlyList<Product>> GetProductsByPriceRange(int startRange, int endRange)
        {
            throw new NotImplementedException();
        }
    }
}
