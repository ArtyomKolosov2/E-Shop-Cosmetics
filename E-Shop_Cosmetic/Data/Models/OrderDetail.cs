using E_Shop_Cosmetic.Data.AbstractClasses;

namespace E_Shop_Cosmetic.Data.Models
{
    public class OrderDetail : Entity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public double PriceOnOrderTime { get; set; }
        public double TotalPrice { get; set; }
        public uint Amount { get; set; }
        public int OrderId { get; set; }
    }
}
