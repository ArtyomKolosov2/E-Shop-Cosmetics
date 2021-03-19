using E_Shop_Cosmetic.Data.AbstractClasses;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        public Task<IReadOnlyList<TEntity>> GetAll();
        public Task<IReadOnlyList<TEntity>> GetAll(ISpecification<TEntity> specification);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> GetById(int id);
    }
}
