using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Specifications;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _cosmeticProductsRepository;
        private readonly ICategoriesRepository _allCategories;
        private readonly ILogger _logger;

        public ProductsController(IProductsRepository products, ICategoriesRepository category, ILogger<ProductsController> logger)
        {
            _cosmeticProductsRepository = products;
            _allCategories = category;
            _logger = logger;
        }
        public async Task<IActionResult> ViewProducts()
        {
            ViewBag.Title = "Товары";
            ProductsViewModel viewModel = new ProductsViewModel();
            viewModel.GetProducts = await _cosmeticProductsRepository.GetProductsAsync(new ProductSpecification().IncludeCategory());
            viewModel.ProductCategory = "Косметика";
            _logger.LogInformation("Products\\ViewProducts is executed");
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Product(int id)
        {
            _logger.LogInformation("Products\\Product is executed");
            return View(await _cosmeticProductsRepository.GetProductByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchParams searchParams)
        {
            var products = await _cosmeticProductsRepository.GetProductsAsync(new ProductSpecification().IncludeCategory());
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
            await _cosmeticProductsRepository.AddProductAsync(newProduct);
            return RedirectToAction("ViewProducts", "Products");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var searchResult = await _cosmeticProductsRepository.GetProductByIdAsync(id);
            if (searchResult is null)
            {
                return NoContent();
            }
            ViewBag.Categories = new SelectList(_allCategories.Categories, "Id", "CategoryName");
            return View(searchResult);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _cosmeticProductsRepository.UpdateProductAsync(product);
            return RedirectToAction("ViewProducts", "Products");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var searchResult = await _cosmeticProductsRepository.GetProductByIdAsync(id);
            if (searchResult is null)
            {
                return NoContent();
            }
            
            return View(searchResult);
        }

        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id, IFormCollection collection)
        {
            var product = await _cosmeticProductsRepository.GetProductByIdAsync(id);
            await _cosmeticProductsRepository.DeleteProductAsync(product); 
            return RedirectToAction("ViewProducts", "Products");
        }

    }
}
