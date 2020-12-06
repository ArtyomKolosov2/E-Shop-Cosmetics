﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Models
{
    public class SearchParams
    {
        public int? SearchProductId { get; set; }
        public string Name { get; set; }
        public double StartPrice { get; set; }
        public double EndPrice { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
    }
}