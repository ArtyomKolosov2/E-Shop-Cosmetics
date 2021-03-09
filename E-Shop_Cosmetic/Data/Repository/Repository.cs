using E_Shop_Cosmetic.Data.AbstractClasses;
using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Specifications;
using E_Shop_Cosmetic.Data.Specifications.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected AppDBContext Context { get; set; }
        private DbSet<T> EntitySet => Context.Set<T>();

        public Repository(AppDBContext appDBContext)
        {
            Context = appDBContext;
        }

        public async Task<T> Add(T entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity; 
        }

        public Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return EntitySet.Where(predicate).CountAsync();
        }

        public Task Delete(T entity)
        {
            EntitySet.Remove(entity);
            return Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await EntitySet.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAll(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await EntitySet.FindAsync(id);
        }

        public Task Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        protected IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator.ApplySpecification(Context.Set<T>().AsQueryable(), spec);
        }
    }
}
