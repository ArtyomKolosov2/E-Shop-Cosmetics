using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Shop_Cosmetic.Data;
using E_Shop_Cosmetic.Data.Models;

namespace E_Shop_Cosmetic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProductsApiController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/ProductsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        // GET: api/ProductsApi/MinPrice
        [HttpGet("MinPrice")]
        public async Task<JsonResult> GetMinPrice()
        {
            return new JsonResult(new {Min = await _context.Products.MinAsync(p => p.Price) });
        }
        // GET: api/ProductsApi/MaxPrice
        [HttpGet("MaxPrice")]
        public async Task<JsonResult> GetMaxPrice()
        {
            return new JsonResult(new { Max = await _context.Products.MaxAsync(p => p.Price) });
        }

        // GET: api/ProductsApi/MaxPrice
        [HttpGet("MinMaxPrice")]
        public async Task<JsonResult> GetMinMaxPrice()
        {
            return new JsonResult(new { Max = await _context.Products.MaxAsync(p => p.Price), Min = await _context.Products.MinAsync(p => p.Price) });
        }
        // GET: api/ProductsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

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
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductsApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/ProductsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

/*adress
wish
phone
cost*/