using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace E_Shop_Cosmetic.Data.Specifications
{
    public class OrderSpecification : Specification<Order>
    {
        public OrderSpecification() : base() { }

        public OrderSpecification(int id) : this(order => order.Id == id) { }

        public OrderSpecification(string address) : this(order => order.Address.ToLower().Contains(address.ToLower())) { }

        public OrderSpecification(Expression<Func<Order, bool>> expression) : base(expression) { }


        public OrderSpecification SortByName()
        {
            AddDescendingOrdering(order => order.Name);
            return this;
        }

        public OrderSpecification SortByLastName()
        {
            AddDescendingOrdering(order => order.LastName);
            return this;
        }

        public OrderSpecification SortByTotalPrice()
        {
            AddDescendingOrdering(order => order.TotalPrice);
            return this;
        }

        public OrderSpecification IncludeDetails(string includeString)
        {
            AddInclude(includeString);
            return this;
        }
        public OrderSpecification IncludeDetails()
        {
            AddInclude("OrderDetails.Product");
            return this;
        }
        public OrderSpecification WithoutTracking()
        {
            IsNoTracking = true;
            return this;
        }

        public OrderSpecification WithTracking()
        {
            IsNoTracking = false;
            return this;
        }
    }
}
