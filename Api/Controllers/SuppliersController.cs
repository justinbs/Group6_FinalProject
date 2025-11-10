using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _svc;
    public SuppliersController(ISupplierService svc) => _svc = svc;

    [HttpGet] public Task<List<Supplier>> GetAll() => _svc.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Supplier>> Get(int id)
    {
        var e = await _svc.GetAsync(id);
        return e is null ? NotFound() : Ok(e);
    }

    [HttpPost]
    public async Task<ActionResult<Supplier>> Create(Supplier model)
    {
        var e = await _svc.CreateAsync(model);
        return CreatedAtAction(nameof(Get), new { id = e.Id }, e);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Supplier>> Update(int id, Supplier model)
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
