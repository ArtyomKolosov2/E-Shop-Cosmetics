using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _allCosmeticProducts;
        private readonly ICategoriesRepository _allCategories;
        private readonly ILogger _logger;
        private readonly AppDBContext _dbContext;

        public ProductsController(AppDBContext appDB, IProductsRepository products, ICategoriesRepository category, ILogger<ProductsController> logger)
        {
            _dbContext = appDB;
            _allCosmeticProducts = products;
            _allCategories = category;
            _logger = logger;
        }
        public async Task<IActionResult> ViewProducts()
        {
            ViewBag.Title = "Товары";
            ProductsViewModel viewModel = new ProductsViewModel();
            viewModel.GetProducts = await _allCosmeticProducts.GetProducts(new ProductSpecification().IncludeCategory());
            viewModel.ProductCategory = "Косметика";
            _logger.LogInformation("Products\\ViewProducts is executed");
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Product(int id)
        {
            _logger.LogInformation("Products\\Product is executed");
            return View(await _allCosmeticProducts.GetProductByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchParams searchParams)
        {
            var products = await _allCosmeticProducts.GetProducts(new ProductSpecification().IncludeCategory());
            ViewBag.Title = "Искомый товар";
            ProductsViewModel viewModel = new ProductsViewModel
            {
                GetProducts = products.Where(x => x.Name == searchParams.Name),
                ProductCategory = "Косметика"
            };
            _logger.LogInformation("Products\\Search is executed");
            if (!viewModel.GetProducts.Any())
            {
                _logger.LogWarning("Search unsuccesful!");
            }
            return View(viewModel);
        }
        public JsonResult GetMinMaxPrices()
        {
            return Json(new {Max=1000, Min=1});
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_allCategories.Categories, "Id", "CategoryName");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product newProduct)
        {
            await _allCosmeticProducts.AddProduct(newProduct);
            return RedirectToAction("ViewProducts", "Products");
        }

    }
}
