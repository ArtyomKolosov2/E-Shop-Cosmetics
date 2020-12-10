using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Models
{
    public class SearchProductsParams
    {
        [BindingBehavior(BindingBehavior.Optional)]
        public int? SearchProductId { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public string Name { get; set; }

        public double StartPrice { get; set; }
        public double EndPrice { get; set; }

        [BindingBehavior(BindingBehavior.Optional)]
        public bool IsAvailable { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public int CategoryId { get; set; }
    }
}
