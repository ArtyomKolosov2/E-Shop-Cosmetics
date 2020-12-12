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
        public async Task<IActionResult> Search(SearchOrderParams searchParams)
        {
            var searchSpecification = new OrderSpecification();
            var isPrimaryKeyUsed = false;
            if (searchParams.OrderId is not null)
            {
                searchSpecification.WhereId(searchParams.OrderId.Value);
                isPrimaryKeyUsed = true;
            }
            if (!isPrimaryKeyUsed)
            {
                if (searchParams.Name is not null)
                {
                    searchSpecification.WhereName(searchParams.Name);
                }
                if (searchParams.LastName is not null)
                {
                    searchSpecification.WhereLastName(searchParams.LastName);
                }
                if (searchParams.Email is not null)
                {
                    searchSpecification.WhereEmail(searchParams.Email);
                }

                if (searchParams.PhoneNumber is not null)
                {
                    searchSpecification.WherePhone(searchParams.PhoneNumber);
                }
            }
            if (searchParams.IsSortByDateRequired)
            {
                searchSpecification.SortByDate();
            }
            searchSpecification.WhereActive(searchParams.IsOrderActive).WithoutTracking();
            var viewModel = new SearchOrderViewModel()
            {
                Orders = await _orderRepository.GetOrdersAsync(searchSpecification),
                SearchParams = searchParams

            };
            ViewBag.Title = "Поиск";
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> PlaceOrder()
        {
            if (await _cartService.IsAnyProductInCartAsync())
            {
                return View();
            }
            return NoContent();

        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel orderViewModel)
        {
            var ordersDetails = await _cartService.GetOrderDetailsAsync();
            if (ordersDetails.Any())
            {
                var totalPrice = Math.Round(ordersDetails.Sum(detail => detail.TotalPrice), 2);
                var newOrder = new Order()
                {
                    Address = orderViewModel.Address,
                    IsOrderActive = true,
                    Information = orderViewModel.Information ?? "None",
                    Name = orderViewModel.Name,
                    LastName = orderViewModel.LastName,
                    PhoneNumber = orderViewModel.PhoneNumber,
                    TotalPrice = totalPrice,
                    OrderDetails = ordersDetails,
                    OrderDate = DateTime.Now,
                    Email = orderViewModel.Email

                };
                await _orderRepository.AddOrderAsync(newOrder);
                await _cartService.ClearCartAsync();
                return RedirectToAction("OrderSuccessful", newOrder);
            }
            else
            {
                return NoContent();
            }
        }


        public IActionResult OrderSuccessful(Order order)
        {
            return View(order);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order is not null)
            {
                return View(order);
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderRepository.UpdateOrderAsync(order);
            return RedirectToAction("ViewOrders", "Orders");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Order(int id)
        {
            var order = await _orderRepository.GetOrderByIdWithDetailsAsync(id);
            if (order is not null)
            {
                return View(new ViewOrderViewModel { Order = order });
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> ViewOrders()
        {
            var orders = await _orderRepository.GetOrdersAsync
                (
                new OrderSpecification().
                IncludeDetails().
                SortByTotalPrice().
                WithoutTracking()
                );
            var viewModel = new SearchOrderViewModel()
            {
                Orders = orders,
                SearchParams = new SearchOrderParams(),
            };
            return View(viewModel);
        }
    }
}
