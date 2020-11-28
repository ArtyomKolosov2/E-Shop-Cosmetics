using E_Shop_Cosmetic.Data.AbstractClasses;
using E_Shop_Cosmetic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository.Models
{
    public class Order : Entity
    {
        public string Name { get; set; }        
        public string LastName { get; set; }        
        public string Address { get; set; }    
        public string Information { get; set; }
        public double TotalPrice { get; set; }
        public bool IsOrderActive { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public string ProductsIdString { get; set; }        
               
    }
}
