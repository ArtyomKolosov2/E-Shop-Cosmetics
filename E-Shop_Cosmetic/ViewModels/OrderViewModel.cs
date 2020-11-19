using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.ViewModels
{
    /*adress
    wish
    phone
    cost*/
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Не указан адресс")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Не указан телефон")]
        public string Phone { get; set; }

        public string Wish { get; set; }
    }
}
