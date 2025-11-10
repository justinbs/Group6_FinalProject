namespace Api.Contracts;

public class OnHandRow
{
    public int ItemId { get; set; }
    public string Name { get; set; } = "";
    public string Code { get; set; } = "";
    public string? Brand { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal AvailableQty { get; set; }
    public decimal TotalValue => decimal.Round(AvailableQty * UnitPrice, 2);
}

public class LowStockRow : OnHandRow { }
