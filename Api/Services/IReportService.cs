using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services;

public record OnHandDto(int ItemId, string Name, int Quantity, decimal UnitPrice);
public record LowStockDto(int ItemId, string Name, int Quantity);

public interface IReportService
{
    Task<List<OnHandDto>> OnHandAsync();
    Task<List<LowStockDto>> LowStockAsync(int threshold);
}
