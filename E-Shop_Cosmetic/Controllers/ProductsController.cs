using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.ViewModels;
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
        private readonly IProducts _allCosmeticProducts;
        private readonly IProductCategories _allCategories;
        private readonly ILogger _logger;
        private readonly AppDBContext _dbContext;

        public ProductsController(AppDBContext appDB, IProducts products, IProductCategories category, ILogger<ProductsController> logger)
        {
            _dbContext = appDB;
            _allCosmeticProducts = products;
            _allCategories = category;
            _logger = logger;
        }
        public IActionResult ViewProducts()
        {
            ViewBag.Title = "Товары";
            ProductsViewModel viewModel = new ProductsViewModel();
            viewModel.GetProducts = _allCosmeticProducts.GetProducts;
            viewModel.ProductCategory = "Косметика";
            _logger.LogInformation("Products\\ViewProducts is executed");
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Product(int id)
        {
            _logger.LogInformation("Products\\Product is executed");
            return View(_allCosmeticProducts.GetProducts.FirstOrDefault(p => p.Id == id));
        }

        [HttpGet]
        public IActionResult Search(SearchParams searchParams)
        {
            ViewBag.Title = "Искомый товар";
            ProductsViewModel viewModel = new ProductsViewModel
            {
                GetProducts = _allCosmeticProducts.GetProducts.
                Where(x => x.Id == searchParams.SearchProductId),
                ProductCategory = "Косметика"
            };
            _logger.LogInformation("Products\\Search is executed");
            if (!viewModel.GetProducts.Any())
            {
                _logger.LogWarning("Search unsuccesful!");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_allCategories.GetAllCategories, "Id", "CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product newProduct)
        {
            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Products", "ViewProducts");
        }

    }
}
