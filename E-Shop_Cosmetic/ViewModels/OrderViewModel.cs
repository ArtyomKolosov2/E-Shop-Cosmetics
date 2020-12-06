using E_Shop_Cosmetic.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Shop_Cosmetic.ViewModels
{
    /*adress
    wish
    phone
    cost*/
    public class OrderViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указан адрес")]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Не указан телефон")]
        public string PhoneNumber { get; set; } 
        public string Information { get; set; }
        public double TotalPrice { get; set; }
        
    }
}
