using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ItemService : IItemService
{
    private readonly AppDbContext _db;
    public ItemService(AppDbContext db) => _db = db;

    public Task<List<Item>> GetAllAsync() =>
        _db.Items.AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Supplier)
            .OrderBy(x => x.Name)
            .ToListAsync();

    public Task<Item?> GetAsync(int id) =>
        _db.Items.AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Item> CreateAsync(Item entity)
    {
        _db.Items.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Item?> UpdateAsync(int id, Item entity)
    {
        var existing = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null) return null;
        existing.Name = entity.Name;
        existing.Brand = entity.Brand;
        existing.CategoryId = entity.CategoryId;
        existing.SupplierId = entity.SupplierId;
        existing.UnitPrice = entity.UnitPrice;
        // Quantity is updated only through stock movements
        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Items.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null) return false;
        _db.Items.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
