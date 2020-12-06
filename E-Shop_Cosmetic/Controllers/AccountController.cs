﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Shop_Cosmetic.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public AccountController(IUserRepository userRepository, IRoleRepository roleRepository, ILogger<AccountController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogInformation("Http Get Account\\Login called");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            _logger.LogInformation("Http Post Account\\Login called");
            if (ModelState.IsValid)
            {
                var users = await _userRepository.GetUsersAsync();
                var user = users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); 
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var users = await _userRepository.GetUsersAsync();
                var user = users.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new User { Email = model.Email, Password = model.Password, UserRoleId = 1, Role = await _roleRepository.GetRoleByIdAsync(1) };
                    await _userRepository.AddUserAsync(user);

                    await Authenticate(user); 

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var roles = await _roleRepository.GetRolesAsync();
            var roleName = roles.FirstOrDefault(i => i.Id == user.UserRoleId);
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName?.Name ?? "user")
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
