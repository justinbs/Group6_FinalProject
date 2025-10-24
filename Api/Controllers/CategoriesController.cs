using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/categories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _service.GetAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            var created = await _service.CreateAsync(category);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/categories/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> Update(int id, Category category)
        {
            var updated = await _service.UpdateAsync(id, category);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE: api/categories/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
