using E_Shop_Cosmetic.Data.AbstractClasses;
using E_Shop_Cosmetic.Data.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository.Models
{
    public class Order : Entity
    {
        public string Name { get; set; }        
        public string LastName { get; set; }    
        public string Address { get; set; }    
        public string PhoneNumber { get; set; }    
        public string Information { get; set; }
        public double TotalPrice { get; set; }
        public bool IsOrderActive { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public string ProductsString { get; set; }      
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
