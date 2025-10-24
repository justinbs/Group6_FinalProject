using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;
        public CategoryService(AppDbContext db) => _db = db;

        public Task<List<Category>> GetAllAsync() => _db.Categories.OrderBy(c => c.Name).ToListAsync();

        public Task<Category?> GetAsync(int id) => _db.Categories.FindAsync(id).AsTask();

        public async Task<Category> CreateAsync(Category e)
        {
            _db.Categories.Add(e);
            await _db.SaveChangesAsync();
            return e;
        }

        public async Task<Category?> UpdateAsync(int id, Category e)
        {
            var existing = await _db.Categories.FindAsync(id);
            if (existing is null) return null;
            existing.Name = e.Name;
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Categories.FindAsync(id);
            if (existing is null) return false;
            _db.Categories.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
