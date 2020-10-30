using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public ProductsController(IProducts products, IProductCategories category)
        {
            _allCosmeticProducts = products;
            _allCategories = category;
        }
        public IActionResult ViewProducts()
        {
            ViewBag.Title = "Товары";
            ProductsViewModel viewModel = new ProductsViewModel();
            viewModel.GetProducts = _allCosmeticProducts.GetProducts;
            viewModel.ProductCategory = "Косметика";
            return View(viewModel);
        }
        
        [HttpGet]
        public IActionResult Search(SearchParams searchParams)
        {
            ViewBag.Title = "Искомый товар";
            ProductsViewModel viewModel = new ProductsViewModel();
            viewModel.GetProducts = _allCosmeticProducts.GetProducts.
                Where(x => x.id == searchParams.id);
            viewModel.ProductCategory = "Косметика";
            return View(viewModel);
        }

    }
}
