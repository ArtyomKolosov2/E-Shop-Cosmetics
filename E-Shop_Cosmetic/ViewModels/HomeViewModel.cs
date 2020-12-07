using E_Shop_Cosmetic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.ViewModels
{
    public class HomeViewModel
    {
        public string Message { get; set; }
        public IEnumerable<Product> GetProducts{ get; set; }
    }
}
