using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository
{
    public class CategoryRepository : ICategoriesRepository
    {
        private readonly AppDBContext appDBContent;

        public CategoryRepository(AppDBContext appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Category> Categories => appDBContent.Categories;
    }
}
