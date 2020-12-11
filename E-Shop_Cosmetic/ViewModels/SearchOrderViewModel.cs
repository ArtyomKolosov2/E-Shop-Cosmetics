using E_Shop_Cosmetic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.ViewModels
{
    public class SearchOrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public SearchOrderParams SearchParams { get; set; }
    }
}
