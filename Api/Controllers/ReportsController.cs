using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _svc;
    public ReportsController(IReportService svc) => _svc = svc;

    [HttpGet("onhand")]
    public Task<List<OnHandDto>> OnHand() => _svc.OnHandAsync();

    [HttpGet("lowstock")]
    public Task<List<LowStockDto>> LowStock([FromQuery] int threshold = 5) =>
        _svc.LowStockAsync(threshold);
}
