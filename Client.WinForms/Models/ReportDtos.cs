namespace Client.WinForms.Models
{
    public record OnHandDto(int ItemId, string Name, int Quantity, decimal UnitPrice);
    public record LowStockDto(int ItemId, string Name, int Quantity);
}
