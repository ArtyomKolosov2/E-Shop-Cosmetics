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

        public OrderSpecification WhereName(string name)
        {
            AddWhere(order => order.Name.ToLower().Contains(name.ToLower()));
            return this;
        }

        public OrderSpecification WhereLastName(string lastName)
        {
            AddWhere(order => order.LastName.ToLower().Contains(lastName.ToLower()));
            return this;
        }

        public OrderSpecification WhereId(int id)
        {
            AddWhere(order => order.Id == id);
            return this;
        }

        public OrderSpecification WhereEmail(string email)
        {
            AddWhere(order => order.Email.Contains(email));
            return this;
        }

        public OrderSpecification WherePhone(string phone)
        {
            AddWhere(order => order.PhoneNumber.Contains(phone));
            return this;
        }

        public OrderSpecification WhereActive(bool isActive)
        {
            AddWhere(order => order.IsOrderActive == isActive);
            return this;
        }

        public OrderSpecification SortByDate()
        {
            AddDescendingOrdering(order => order.OrderDate);
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
