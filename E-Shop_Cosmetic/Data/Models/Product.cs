using E_Shop_Cosmetic.Data.AbstractClasses;
using System.ComponentModel.DataAnnotations;

namespace E_Shop_Cosmetic.Data.Models
{
    public class Product : Entity
    {
        [Required(ErrorMessage = "Имя товара обязательно!")]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        [DataType(DataType.ImageUrl)]
        [Url(ErrorMessage = "Неверный формат")]
        [Required(ErrorMessage = "Добавьте картинку")]
        public string ImageURL { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Цена не указана!")]
        public double Price { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
