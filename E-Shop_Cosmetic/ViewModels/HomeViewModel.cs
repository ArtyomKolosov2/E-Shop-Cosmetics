using E_Shop_Cosmetic.Data.Models;
using System.Collections.Generic;
namespace E_Shop_Cosmetic.ViewModels
{
    public class HomeViewModel
    {
        public string Message { get; set; }
        public IEnumerable<Product> Products{ get; set; }
    }
}
