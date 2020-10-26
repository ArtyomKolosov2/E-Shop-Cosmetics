
using E_Shop_Cosmetic.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.interfaces
{
    public interface IProductCategories
    {
        IEnumerable<Category> GetAllCategories { get; }
    }
}
