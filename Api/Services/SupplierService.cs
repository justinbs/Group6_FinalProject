using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class SupplierService : ISupplierService
{
    private readonly AppDbContext _db;
    public SupplierService(AppDbContext db) => _db = db;

    public Task<List<Supplier>> GetAllAsync() =>
        _db.Suppliers.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

    public Task<Supplier?> GetAsync(int id) =>
        _db.Suppliers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Supplier> CreateAsync(Supplier entity)
    {
        _db.Suppliers.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Supplier?> UpdateAsync(int id, Supplier entity)
    {
        var existing = await _db.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null) return null;
        existing.Name = entity.Name;
        existing.Contact = entity.Contact;
        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null) return false;
        _db.Suppliers.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
