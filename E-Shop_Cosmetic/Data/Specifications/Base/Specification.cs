using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Specifications.Base
{
    public abstract class Specification<T> : ISpecification<T>
    {
        protected Specification()
        {

        }

        protected Specification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();

        public List<Expression<Func<T, object>>> OrderByExpressions { get; private set; } = new List<Expression<Func<T, object>>>();
        public List<Expression<Func<T, object>>> OrderByDescendingExpressions { get; private set; } = new List<Expression<Func<T, object>>>();

        public ISpecification<T> AddPagination(int take = 0, int skip = 0)
        {
            Take = take;
            Skip = skip;

            return this;
        }

        public int Skip { get; set; }
        public int Take { get; set; }

        protected void AddOrdering(Expression<Func<T, object>> expression)
        {
            OrderByExpressions.Add(expression);
        }

        protected void AddDescendingOrdering(Expression<Func<T, object>> expression)
        {
            OrderByDescendingExpressions.Add(expression);
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

    }
}
