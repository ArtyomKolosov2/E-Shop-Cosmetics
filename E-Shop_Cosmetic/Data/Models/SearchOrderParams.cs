using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace E_Shop_Cosmetic.Data.Models
{
    public class SearchOrderParams
    {
        [BindingBehavior(BindingBehavior.Optional)]
        public int? OrderId { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public string Name { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public string LastName { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public string PhoneNumber { get; set; }

        [BindingBehavior(BindingBehavior.Optional)]
        [EmailAddress(ErrorMessage = "Неправильный адрес")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool IsSortByDateRequired { get; set; }
        public bool IsOrderActive { get; set; } = true;
    }
}
