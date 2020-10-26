using E_Shop_Cosmetic.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.interfaces
{
    public interface IProducts
    {
        IEnumerable<Product> GetCosmeticProducts { get; }
        IEnumerable<Product> GetFavoriteCosmetics { get; }
        Product GetProductById(int productId);
    }
}
