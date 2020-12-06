using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDBContext appDBContext) : base(appDBContext)
        {

        }

        public async Task AddUserAsync(User user)
        {
            await AddAsync(user);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<User>> GetUsersAsync()
        {
            return await GetAllAsync();
        }

        public async Task<IReadOnlyList<User>> GetUsersAsync(ISpecification<User> specification)
        {
            return await GetAllAsync(specification);
        }
    }
}
