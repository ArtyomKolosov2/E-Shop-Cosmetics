using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductsByIds(IEnumerable<int> ids);
        Task<Product> GetProductByName(string name);
        Task<Product> GetProductByPrice(int price);
    }
}
