using E_Shop_Cosmetic.Data.Models;
using System.Collections.Generic;

namespace E_Shop_Cosmetic.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public SearchProductsParams SearchParams { get; set; }
        public string ProductCategory { get; set; }
    }
}
