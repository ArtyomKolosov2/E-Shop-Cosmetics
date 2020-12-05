using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICookieCartService _cartService;
        public OrdersController(ICookieCartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderViewModel orderViewModel)
        {
            var ordersDetails = _cartService.GetOrderDetails();
            return View();
        }
    }
}
