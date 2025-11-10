using System.Threading.Tasks;

namespace Api.Services;

public interface IStockService
{
    Task<bool> ReceiveAsync(int itemId, int quantity, string? note);
    Task<bool> IssueAsync(int itemId, int quantity, string? note);
}
