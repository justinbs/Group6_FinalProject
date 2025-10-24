using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SuppliersController(ISupplierService service)
        {
            _service = service;
        }

        // GET: api/suppliers
        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetAll()
        {
            var suppliers = await _service.GetAllAsync();
            return Ok(suppliers);
        }

        // GET: api/suppliers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Supplier>> Get(int id)
        {
            var supplier = await _service.GetAsync(id);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }

        // POST: api/suppliers
        [HttpPost]
        public async Task<ActionResult<Supplier>> Create(Supplier supplier)
        {
            var created = await _service.CreateAsync(supplier);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/suppliers/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Supplier>> Update(int id, Supplier supplier)
        {
            var updated = await _service.UpdateAsync(id, supplier);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE: api/suppliers/5
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
