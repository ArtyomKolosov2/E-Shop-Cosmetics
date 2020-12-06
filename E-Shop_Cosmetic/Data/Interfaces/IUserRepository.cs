using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetUsersAsync();
        Task<IReadOnlyList<User>> GetUsersAsync(ISpecification<User> specification);
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
    }
}
