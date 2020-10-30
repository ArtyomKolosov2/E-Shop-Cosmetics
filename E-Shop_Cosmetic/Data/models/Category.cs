using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Models
{
    public class Category
    {
        public int id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Product> ProductsOfCurrentCategory { get; set; }
    }
}
