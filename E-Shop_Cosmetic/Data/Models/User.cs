﻿using E_Shop_Cosmetic.Data.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Models
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}