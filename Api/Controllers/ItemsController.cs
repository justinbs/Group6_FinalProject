using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _svc;
    public ItemsController(IItemService svc) => _svc = svc;

    [HttpGet] public Task<List<Item>> GetAll() => _svc.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Item>> Get(int id)
    {
        var e = await _svc.GetAsync(id);
        return e is null ? NotFound() : Ok(e);
    }

    [HttpPost]
    public async Task<ActionResult<Item>> Create(Item model)
    {
        var e = await _svc.CreateAsync(model);
        return CreatedAtAction(nameof(Get), new { id = e.Id }, e);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Item>> Update(int id, Item model)
    {
        var e = await _svc.UpdateAsync(id, model);
        return e is null ? NotFound() : Ok(e);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
