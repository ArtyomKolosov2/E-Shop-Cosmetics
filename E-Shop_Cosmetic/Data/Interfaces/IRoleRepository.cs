using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface IRoleRepository
    {
        Task<IReadOnlyList<Role>> GetRolesAsync();
        Task<IReadOnlyList<Role>> GetRolesAsync(ISpecification<Role> specification);
        Task<Role> GetRoleByIdAsync(int id);
    }
}
