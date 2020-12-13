using E_Shop_Cosmetic.Data.Models;
using System.Collections.Generic;

namespace E_Shop_Cosmetic.ViewModels
{
    public class SearchOrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public SearchOrderParams SearchParams { get; set; }
    }
}
