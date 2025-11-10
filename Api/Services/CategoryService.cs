using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _db;
    public CategoryService(AppDbContext db) => _db = db;

    public Task<List<Category>> GetAllAsync() =>
        _db.Categories.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

    public Task<Category?> GetAsync(int id) =>
        _db.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Category> CreateAsync(Category entity)
    {
        _db.Categories.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Category?> UpdateAsync(int id, Category entity)
    {
        var existing = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null) return null;
        existing.Name = entity.Name;
        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null) return false;
        _db.Categories.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
