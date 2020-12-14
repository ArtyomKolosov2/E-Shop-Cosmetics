using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace E_Shop_Cosmetic.Data.Specifications
{
    public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification() : base() { }
        public ProductSpecification(Expression<Func<Product, bool>> expression) : base(expression) { }

        public ProductSpecification SortByName()
        {
            AddDescendingOrdering(product => product.Name);
            return this;
        }

        public ProductSpecification WhereInPriceRange(double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min is greater than max!");
            }
            AddWhere(product => product.Price >= min && product.Price <= max);
            return this;
        }

        public ProductSpecification WhereId(int id)
        {
            AddWhere(product => product.Id == id);
            return this;
        }

        public ProductSpecification WhereCategoryId(int id)
        {
            AddWhere(product => product.CategoryId == id);
            return this;
        }

        public ProductSpecification WhereAvailable(bool isAvailable)
        {
            AddWhere(product => product.IsAvailable == isAvailable);
            return this;
        }
        public ProductSpecification WhereName(string name)
        {
            AddWhere(product => product.Name.ToLower().Contains(name.ToLower()));
            return this;
        }
        public ProductSpecification SortByPrice()
        {
            AddDescendingOrdering(product => product.Price);
            return this;
        }

        public ProductSpecification IncludeCategory()
        {
            AddInclude(product => product.Category);
            return this;
        }
        public ProductSpecification WithoutTracking()
        {
            IsNoTracking = true;
            return this;
        }

        public ProductSpecification WithTracking()
        {
            IsNoTracking = false;
            return this;
        }
    }
}
