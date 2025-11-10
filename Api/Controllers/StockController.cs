using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public record StockMoveDto(int ItemId, int Quantity, string? Note);

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _svc;
    public StockController(IStockService svc) => _svc = svc;

    [HttpPost("in")]
    public async Task<IActionResult> Receive(StockMoveDto dto)
    {
        var ok = await _svc.ReceiveAsync(dto.ItemId, dto.Quantity, dto.Note);
        return ok ? Ok() : BadRequest();
    }

    [HttpPost("out")]
    public async Task<IActionResult> Issue(StockMoveDto dto)
    {
        var ok = await _svc.IssueAsync(dto.ItemId, dto.Quantity, dto.Note);
        return ok ? Ok() : BadRequest();
    }
}
