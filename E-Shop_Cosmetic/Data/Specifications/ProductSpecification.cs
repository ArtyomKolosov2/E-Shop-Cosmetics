using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Specifications
{
    public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification() : base() { }

        public ProductSpecification(int id) : this(product => product.Id == id) { }

        public ProductSpecification(string name) : this(product => product.Name.ToLower().Contains(name.ToLower())) { }

        public ProductSpecification(Expression<Func<Product, bool>> expression) : base(expression) { }


        public ProductSpecification SortByName()
        {
            AddDescendingOrdering(product => product.Name);
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
    }
}
