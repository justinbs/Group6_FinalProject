using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly AppDbContext _db;
        public StockMovementService(AppDbContext db) => _db = db;

        public Task<List<StockMovement>> GetAllAsync() =>
            _db.StockMovements.Include(m => m.Item).OrderByDescending(m => m.Date).ToListAsync();

        public Task<StockMovement?> GetAsync(int id) =>
            _db.StockMovements.Include(m => m.Item).FirstOrDefaultAsync(m => m.Id == id);

        public async Task<StockMovement> CreateAndApplyAsync(StockMovement m)
        {
            if (m.Quantity <= 0) throw new ArgumentException("Quantity must be positive.");

            var item = await _db.Items.FindAsync(m.ItemId)
                       ?? throw new ArgumentException("Item not found.");

            var delta = m.Type switch
            {
                MovementType.Purchase => +m.Quantity,
                MovementType.Sale => -m.Quantity,
                MovementType.Adjustment => +m.Quantity,
                _ => 0
            };

            if (m.Type == MovementType.Sale && item.Quantity < m.Quantity)
                throw new InvalidOperationException("Insufficient stock.");

            item.Quantity += delta;

            _db.StockMovements.Add(m);
            await _db.SaveChangesAsync();
            return m;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.StockMovements.FindAsync(id);
            if (existing is null) return false;
            _db.StockMovements.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
