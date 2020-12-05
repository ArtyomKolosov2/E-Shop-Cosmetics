using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace E_Shop_Cosmetic.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]   
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsApiController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: api/ProductsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productsRepository.GetProductsAsync();
            return new ActionResult<IEnumerable<Product>>(products);
        }
        // GET: api/ProductsApi/MinPrice
        [HttpGet("MinPrice")]
        public async Task<JsonResult> GetMinPrice()
        {
            var products = await _productsRepository.GetProductsAsync();
            return new JsonResult(new {Min = products.Min(p => p.Price) });
        }
        // GET: api/ProductsApi/MaxPrice
        [HttpGet("MaxPrice")]
        public async Task<JsonResult> GetMaxPrice()
        {
            var products = await _productsRepository.GetProductsAsync();
            return new JsonResult(new { Max = products.Max(p => p.Price) });
        }

        // GET: api/ProductsApi/MaxPrice
        [HttpGet("MinMaxPrice")]
        public async Task<JsonResult> GetMinMaxPrice()
        {
            var products = await _productsRepository.GetProductsAsync();
            return new JsonResult(new { Max = products.Max(p => p.Price), Min = products.Min(p => p.Price) });
        }
        // GET: api/ProductsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productsRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/ProductsApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productsRepository.UpdateProductAsync(product);
            return NoContent();
        }

        // POST: api/ProductsApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _productsRepository.AddProductAsync(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/ProductsApi/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _productsRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productsRepository.DeleteProductAsync(product);

            return product;
        }

        private async Task<bool> ProductExists(int id)
        {
            var products = await _productsRepository.GetProductsAsync();
            return  products.Any(e => e.Id == id);
        }
    }
}