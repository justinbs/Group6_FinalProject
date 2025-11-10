using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _db;
    public ReportService(AppDbContext db) => _db = db;

    public Task<List<OnHandDto>> OnHandAsync() =>
        _db.Items.AsNoTracking()
            .Select(x => new OnHandDto(x.Id, x.Name, x.Quantity, x.UnitPrice))
            .OrderBy(x => x.Name)
            .ToListAsync();

    public Task<List<LowStockDto>> LowStockAsync(int threshold) =>
        _db.Items.AsNoTracking()
            .Where(x => x.Quantity <= threshold)
            .Select(x => new LowStockDto(x.Id, x.Name, x.Quantity))
            .OrderBy(x => x.Quantity)
            .ThenBy(x => x.Name)
            .ToListAsync();
}
