using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class StockService : IStockService
{
    private readonly AppDbContext _db;
    public StockService(AppDbContext db) => _db = db;

    public async Task<bool> ReceiveAsync(int itemId, int quantity, string? note)
    {
        if (quantity <= 0) return false;
        var item = await _db.Items.FirstOrDefaultAsync(x => x.Id == itemId);
        if (item is null) return false;

        item.Quantity += quantity;
        _db.StockLedgers.Add(new StockLedger
        {
            ItemId = itemId,
            Quantity = quantity,
            Type = MovementType.In,
            Note = note ?? "Receive"
        });

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IssueAsync(int itemId, int quantity, string? note)
    {
        if (quantity <= 0) return false;
        var item = await _db.Items.FirstOrDefaultAsync(x => x.Id == itemId);
        if (item is null) return false;
        if (item.Quantity < quantity) return false;

        item.Quantity -= quantity;
        _db.StockLedgers.Add(new StockLedger
        {
            ItemId = itemId,
            Quantity = quantity,
            Type = MovementType.Out,
            Note = note ?? "Issue"
        });

        await _db.SaveChangesAsync();
        return true;
    }
}
