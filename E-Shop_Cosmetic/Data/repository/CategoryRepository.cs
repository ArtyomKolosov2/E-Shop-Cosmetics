using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoriesRepository
    {
        public CategoryRepository(AppDBContext appDBContext) : base(appDBContext)
        {
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            return await GetAllAsync();
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync(ISpecification<Category> specification)
        {
            return await GetAllAsync(specification);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }
    }
}
