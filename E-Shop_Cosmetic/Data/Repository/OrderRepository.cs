using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications;
using E_Shop_Cosmetic.Data.Specifications.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDBContext appDBContext) : base(appDBContext)
        {

        }

        public async Task<Order> GetOrderByIdWithDetailsOrDefault(int id)
        {
            var spec = new OrderSpecification(id)
                .IncludeDetails("OrderDetails.Product.Category");

            var orders = await GetAll(spec);

            return orders.FirstOrDefault();
        }
    }
}
