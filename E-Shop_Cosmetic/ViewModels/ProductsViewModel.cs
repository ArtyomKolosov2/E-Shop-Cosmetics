using E_Shop_Cosmetic.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> GetProducts { get; set; }

        public string ProductCategory { get; set; }
    }
}
