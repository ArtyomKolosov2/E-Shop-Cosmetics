using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Interfaces;

namespace E_Shop_Cosmetic.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly IRepository<Category> _categoriesRepository;

        public CategoriesApiController(IRepository<Category> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        // GET: api/CategoriesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return new ActionResult<IEnumerable<Category>>(await _categoriesRepository.GetAll());
        }

        // GET: api/CategoriesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoriesRepository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            await _categoriesRepository.Update(category);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _categoriesRepository.Add(category);

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/CategoriesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoriesRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
