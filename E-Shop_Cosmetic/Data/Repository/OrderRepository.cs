using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDBContext appDBContext) : base(appDBContext)
        {

        }
        public async Task AddOrderAsync(Order order)
        {
            await AddAsync(order);
        }

        public async Task DeleteOrderAsync(Order order)
        {
            await DeleteAsync(order);
        }
        public async Task UpdateOrderAsync(Order order)
        {
            await UpdateAsync(order);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync()
        {
            return await GetAllAsync();
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(ISpecification<Order> specification)
        {
            return await GetAllAsync(specification);
        }
    }
}
