using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace E_Shop_Cosmetic.Data.Specifications
{
    public class CategorySpecification : Specification<Category>
    {
        public CategorySpecification() : base() { }
        public CategorySpecification(int id) : this(category => category.Id == id) { }
        public CategorySpecification(string name) : this(category => category.CategoryName.ToLower().Contains(name.ToLower())) { }
        public CategorySpecification(Expression<Func<Category, bool>> expression) : base(expression) { }
        public CategorySpecification SortByName()
        {
            AddDescendingOrdering(category => category.CategoryName);
            return this;
        }
        public CategorySpecification WithoutTracking()
        {
            IsNoTracking = true;
            return this;
        }

        public CategorySpecification WithTracking()
        {
            IsNoTracking = false;
            return this;
        }
    }
}
