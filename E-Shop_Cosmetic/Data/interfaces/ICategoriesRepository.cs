using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
        Task<IReadOnlyList<Category>> GetCategoriesAsync(ISpecification<Category> specification);
        Task<Category> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(Category category);
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
    }
}
