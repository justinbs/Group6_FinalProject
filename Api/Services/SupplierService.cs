using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _db;
        public SupplierService(AppDbContext db) => _db = db;

        public Task<List<Supplier>> GetAllAsync() => _db.Suppliers.OrderBy(s => s.Name).ToListAsync();

        public Task<Supplier?> GetAsync(int id) => _db.Suppliers.FindAsync(id).AsTask();

        public async Task<Supplier> CreateAsync(Supplier e)
        {
            _db.Suppliers.Add(e);
            await _db.SaveChangesAsync();
            return e;
        }

        public async Task<Supplier?> UpdateAsync(int id, Supplier e)
        {
            var existing = await _db.Suppliers.FindAsync(id);
            if (existing is null) return null;
            existing.Name = e.Name;
            existing.Contact = e.Contact;
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Suppliers.FindAsync(id);
            if (existing is null) return false;
            _db.Suppliers.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
