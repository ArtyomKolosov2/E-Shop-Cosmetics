using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface IProductsRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<Product>> GetProductsAsync(ISpecification<Product> specification);
        Task<IReadOnlyList<Product>> GetFavoriteProductsAsync();
        Task<IReadOnlyList<Product>> GetProductsByPriceRange(int startRange, int endRange);
        Task<Product> GetProductByIdAsync(int productId);
        Task<IReadOnlyList<Product>> GetProductsByIdsAsync(IEnumerable<int> ids);
        Task<Product> GetProductByNameAsync(string name);
        Task<Product> GetProductByPriceAsync(int price);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
