using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Other;
using E_Shop_Cosmetic.Data.Specifications;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _cosmeticProductsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly ILogger _logger;

        public ProductsController(IProductsRepository products, IRepository<Category> category, ILogger<ProductsController> logger)
        {
            _cosmeticProductsRepository = products;
            _categoriesRepository = category;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> Product(int id)
        {
            ViewBag.Title = "Продукт";
            _logger.LogInformation("Products\\Product is executed");
            var product = await _cosmeticProductsRepository.GetById(id);

            if (product is not null)
            {
                return View(product);
            }

            return NoContent();
        }

        public async Task<IActionResult> ViewProducts()
        {
            var viewModel = new ProductsViewModel
            {
                Products = await _cosmeticProductsRepository.GetAll(
                    new ProductSpecification().
                             IncludeCategory().
                             WithoutTracking()),

                ProductCategory = "Косметика",
                SearchParams = new SearchProductsParams()
            };

            _logger.LogInformation("Products\\ViewProducts is executed");

            var categories = await _categoriesRepository.GetAll();

            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            ViewBag.Title = "Вывод продуктов";

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchProductsParams searchParams)
        {
            ViewBag.Title = "Поиск";
            var searchSpecification = new ProductSpecification().IncludeCategory();

            if (searchParams.StartPrice is not null && searchParams.EndPrice is not null)
            {
                searchSpecification.WhereInPriceRange(searchParams.StartPrice.Value, searchParams.EndPrice.Value);
            }

            var isPrimeKeyUsed = false;

            if (searchParams.SearchProductId is not null)
            {
                searchSpecification.WhereId(searchParams.SearchProductId.Value);
                isPrimeKeyUsed = true;
            }

            if (!isPrimeKeyUsed) 
            {

                if (searchParams.Name is not null)
                {
                    searchSpecification.WhereName(searchParams.Name);
                }

                if (searchParams.IsSortByPriceRequired)
                {
                    searchSpecification.SortByPrice();
                }

                if (searchParams.CategoryId is not null)
                {
                    searchSpecification.WhereCategoryId(searchParams.CategoryId.Value);
                }
            }

            searchSpecification.WhereAvailable(searchParams.IsAvailable)
                               .WithoutTracking();

            ViewBag.Title = "Искомый товар";
            ViewBag.Categories = new SelectList(await _categoriesRepository.GetAll(), "Id", "CategoryName");

            var viewModel = new ProductsViewModel
            {
                Products = await _cosmeticProductsRepository.GetAll(searchSpecification),
                ProductCategory = "Косметика",
                SearchParams = new SearchProductsParams()
            };

            _logger.LogInformation("Products\\Search is executed");

            if (!viewModel.Products.Any())
            {
                _logger.LogWarning("Search unsuccesful!");
            }

            return View(viewModel);
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ViewBag.Title = "Добавление продукта";
            ViewBag.Categories = new SelectList(await _categoriesRepository.GetAll(), "Id", "CategoryName");

            return View();
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product newProduct)
        {
            newProduct.Price = Math.Round(newProduct.Price, 2);

            await _cosmeticProductsRepository.Add(newProduct);

            return RedirectToAction("ViewProducts", "Products");
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.Title = "Изменение продукта";
            var searchResult = await _cosmeticProductsRepository.GetById(id);

            if (searchResult is null)
            {
                return NoContent();
            }

            ViewBag.Categories = new SelectList(await _categoriesRepository.GetAll(), "Id", "CategoryName");

            return View(searchResult);
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            product.Price = Math.Round(product.Price, 2);
            await _cosmeticProductsRepository.Update(product);

            return RedirectToAction("ViewProducts", "Products");
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ViewBag.Title = "Удаление продукта";
            var searchResult = await _cosmeticProductsRepository.GetById(id);

            if (searchResult is null)
            {
                return NoContent();
            }

            return View(searchResult);
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id, IFormCollection collection)
        {
            var product = await _cosmeticProductsRepository.GetById(id);
            await _cosmeticProductsRepository.Delete(product); 

            return RedirectToAction("ViewProducts", "Products");
        }
    }
}
