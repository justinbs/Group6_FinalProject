using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IStockMovementService
    {
        Task<List<StockMovement>> GetAllAsync();
        Task<StockMovement?> GetAsync(int id);
        Task<StockMovement> CreateAndApplyAsync(StockMovement movement);
        Task<bool> DeleteAsync(int id);
    }
}
