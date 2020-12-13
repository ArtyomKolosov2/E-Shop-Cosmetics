using E_Shop_Cosmetic.Data.AbstractClasses;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Shop_Cosmetic.Data.Models
{
    public class Order : Entity
    {
        public string Name { get; set; }        
        public string LastName { get; set; }   
        
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Неверный адрес!")]
        public string Email { get; set; }    
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }    
        public string Information { get; set; }
        [Required(ErrorMessage ="Нет цены!")]
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
