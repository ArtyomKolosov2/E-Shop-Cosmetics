using E_Shop_Cosmetic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface IProducts
    {
        IEnumerable<Product> GetProducts { get; }
        IEnumerable<Product> GetFavoriteProducts { get; }
        Product GetProductById(int productId);
    }
}
