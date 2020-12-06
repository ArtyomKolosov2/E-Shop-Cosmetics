using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IOrderRepository _orderRepository;
        public OrdersController(ICookieService cartService, IOrderRepository orderRepository)
        {
            _cartService = cartService;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel orderViewModel)
        {
            var ordersDetails = await _cartService.GetOrderDetailsAsync();
            var newOrder = new Order()
            {
                Address = orderViewModel.Address,
                IsOrderActive = true,
                Information = orderViewModel.Information ?? "None",
                Name = orderViewModel.Name,
                LastName = orderViewModel.LastName,
                PhoneNumber = orderViewModel.PhoneNumber,
                TotalPrice = orderViewModel.TotalPrice,
                OrderDetails = ordersDetails,
                OrderDate = DateTime.Now,

            };
            await _orderRepository.AddOrderAsync(newOrder);
            return RedirectToAction("OrderSuccessful", newOrder);
        }
        [HttpGet]
        public IActionResult OrderSuccessful(Order order)
        {
            return View(order);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> ViewOrders()
        {
            return View(await _orderRepository.GetOrdersAsync(new OrderSpecification().IncludeDetails().SortByTotalPrice()));
        }
    }
}
