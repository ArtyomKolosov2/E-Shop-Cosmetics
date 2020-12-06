using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Specifications
{
    public class RoleSpecification : Specification<Role>
    {
        public RoleSpecification() : base() { }
        public RoleSpecification(int id) : this(role => role.Id == id) { }
        public RoleSpecification(string name) : this(role => role.Name.ToLower().Contains(name.ToLower())) { }
        public RoleSpecification(Expression<Func<Role, bool>> expression) : base(expression) { }
        public RoleSpecification SortByName()
        {
            AddDescendingOrdering(role => role.Name);
            return this;
        }
    }
}
