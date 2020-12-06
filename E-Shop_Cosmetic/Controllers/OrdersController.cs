using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Repository.Models;
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
        private readonly ICookieService _cartService;
        private readonly AppDBContext appDB;
        public OrdersController(ICookieService cartService, AppDBContext appDBContext)
        {
            _cartService = cartService;
            appDB = appDBContext;
        }

        [HttpGet]
        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel orderViewModel) //ToDo replace with order repository
        {
            var ordersDetails = await _cartService.GetOrderDetailsAsync();
            var newOrder = new Order()
            {
                Address = orderViewModel.Address,
                IsOrderActive = true,
                Information = orderViewModel.Information ?? "NONE",
                Name = orderViewModel.Name,
                LastName = orderViewModel.LastName,
                PhoneNumber = orderViewModel.PhoneNumber,
                TotalPrice = orderViewModel.TotalPrice,
                OrderDetails = ordersDetails,
                OrderDate = DateTime.Now,

            };
            await appDB.Orders.AddAsync(newOrder);
            await appDB.SaveChangesAsync();
            return RedirectToAction("OrderSuccessful", newOrder);
        }
        [HttpGet]
        public IActionResult OrderSuccessful(Order order)
        {
            return View(order);
        }
    }
}
