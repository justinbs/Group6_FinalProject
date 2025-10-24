using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockMovementsController : ControllerBase
    {
        private readonly IStockMovementService _service;

        public StockMovementsController(IStockMovementService service)
        {
            _service = service;
        }

        // GET: api/stockmovements
        [HttpGet]
        public async Task<ActionResult<List<StockMovement>>> GetAll()
        {
            var movements = await _service.GetAllAsync();
            return Ok(movements);
        }

        // GET: api/stockmovements/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StockMovement>> Get(int id)
        {
            var movement = await _service.GetAsync(id);
            if (movement == null)
                return NotFound();
            return Ok(movement);
        }

        // POST: api/stockmovements
        [HttpPost]
        public async Task<ActionResult<StockMovement>> Create(StockMovement movement)
        {
            var created = await _service.CreateAndApplyAsync(movement);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // DELETE: api/stockmovements/5
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
