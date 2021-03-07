using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;
using E_Shop_Cosmetic.Data.Specifications;
using Microsoft.AspNetCore.Identity;
using E_Shop_Cosmetic.Data.Other;

namespace E_Shop_Cosmetic.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Title = "Логин";
            _logger.LogInformation("Http Get Account\\Login called");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            _logger.LogInformation("Http Post Account\\Login called");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);

                if (user is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Title = "Регистрация";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var newUser = new User
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                };

                var identityResult = await _userManager.CreateAsync(newUser, viewModel.Password);
                if (identityResult.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(newUser, viewModel.Password, false, false);
                    await _userManager.AddToRoleAsync(newUser, IdentityRoleConstants.User);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(viewModel);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
