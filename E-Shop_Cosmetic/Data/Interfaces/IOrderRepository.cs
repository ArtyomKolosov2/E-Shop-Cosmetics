using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetOrdersAsync();
        Task<IReadOnlyList<Order>> GetOrdersAsync(ISpecification<Order> specification);
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }
}
