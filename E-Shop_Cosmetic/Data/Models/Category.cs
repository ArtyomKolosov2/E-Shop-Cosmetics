using E_Shop_Cosmetic.Data.AbstractClasses;
using System.Collections.Generic;

namespace E_Shop_Cosmetic.Data.Models
{
    public class Category : Entity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Product> ProductsOfCurrentCategory { get; set; }
    }
}
