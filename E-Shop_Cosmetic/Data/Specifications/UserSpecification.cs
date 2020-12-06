using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace E_Shop_Cosmetic.Data.Specifications
{
    public class UserSpecification : Specification<User>
    {
        public UserSpecification() : base() { }
        public UserSpecification(int id) : this(user => user.Id == id) { }
        public UserSpecification(string name) : this(user => user.Email.ToLower().Contains(name.ToLower())) { }
        public UserSpecification(Expression<Func<User, bool>> expression) : base(expression) { }
        public UserSpecification SortByEmail()
        {
            AddDescendingOrdering(category => category.Email);
            return this;
        }
        public UserSpecification IncludeRole()
        {
            AddInclude(user => user.Role);
            return this;
        }
    }
}
