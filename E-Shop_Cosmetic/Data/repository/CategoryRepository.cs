using E_Shop_Cosmetic.Data.interfaces;
using E_Shop_Cosmetic.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.repository
{
    public class CategoryRepository : IProductCategories
    {
        private readonly AppDBContent appDBContent;

        public CategoryRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Category> GetAllCategories => appDBContent.Categories;
    }
}
