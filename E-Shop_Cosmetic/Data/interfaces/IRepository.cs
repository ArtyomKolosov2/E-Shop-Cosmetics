using E_Shop_Cosmetic.Data.AbstractClasses;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        public Task<IReadOnlyList<T>> GetAll();
        public  Task<IReadOnlyList<T>> GetAll(ISpecification<T> specification);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(int id);
        Task<int> Count(Expression<Func<T, bool>> predicate);
    }
}
