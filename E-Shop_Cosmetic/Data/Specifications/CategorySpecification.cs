using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Specifications
{
    public class CategorySpecification : Specification<Category>
    {
        public CategorySpecification() : base() { }
        public CategorySpecification(int id) : this(product => product.Id == id) { }
        public CategorySpecification(string name) : this(product => product.CategoryName.ToLower().Contains(name.ToLower())) { }
        public CategorySpecification(Expression<Func<Category, bool>> expression) : base(expression) { }
        public CategorySpecification SortByName()
        {
            AddDescendingOrdering(category => category.CategoryName);
            return this;
        }
    }
}
