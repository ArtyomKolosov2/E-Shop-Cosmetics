using E_Shop_Cosmetic.Data.AbstractClasses;
using E_Shop_Cosmetic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Repository.Models
{
    public class OrderDetail : Entity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public double PriceOnOrderTime { get; set; }
        public uint Amount { get; set; }
        public int OrderId { get; set; }
    }
}
