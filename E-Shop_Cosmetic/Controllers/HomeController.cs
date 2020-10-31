using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Main";
            var obj = new HomeViewModel
            {
                Message = $"Приветствую на сайте, {User.Identity.Name}"
            };
            return View(obj);
        }
    }
}
