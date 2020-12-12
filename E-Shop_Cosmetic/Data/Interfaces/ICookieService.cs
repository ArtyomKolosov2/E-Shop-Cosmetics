using E_Shop_Cosmetic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Interfaces
{
    public interface ICookieService
    {
        Task<List<OrderDetail>> GetOrderDetailsAsync();
        Task<bool> IsAnyProductInCartAsync();
        Task ClearCartAsync();
    }
}
