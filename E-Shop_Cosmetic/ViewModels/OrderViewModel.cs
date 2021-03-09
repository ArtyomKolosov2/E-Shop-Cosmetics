using System.ComponentModel.DataAnnotations;

namespace E_Shop_Cosmetic.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
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
