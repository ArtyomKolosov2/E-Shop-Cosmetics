using E_Shop_Cosmetic.Data.AbstractClasses;
using System.ComponentModel.DataAnnotations;

namespace E_Shop_Cosmetic.Data.Models
{
    public class User : Entity
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int? UserRoleId { get; set; }
        public Role Role { get; set; }
    }
}
