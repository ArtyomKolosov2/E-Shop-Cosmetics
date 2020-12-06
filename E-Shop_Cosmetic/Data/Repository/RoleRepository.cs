using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AppDBContext appDBContext) : base(appDBContext)
        {

        }
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Role>> GetRolesAsync()
        {
            return await GetAllAsync();
        }

        public async Task<IReadOnlyList<Role>> GetRolesAsync(ISpecification<Role> specification)
        {
            return await GetAllAsync(specification);
        }
    }
}
