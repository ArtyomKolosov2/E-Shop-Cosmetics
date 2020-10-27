using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.models
{
    public class SearchParams
    {
        public int id { get; set; }
        public string Name { get; set; }
        public uint Price { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
    }
}
