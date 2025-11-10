namespace Api.Contracts;

public class StockInDto
{
    public int ItemId { get; set; }
    public decimal Qty { get; set; }
    public decimal UnitCost { get; set; }
    public string? RefNo { get; set; }
    public string? Remarks { get; set; }
}

public class StockOutDto
{
    public int ItemId { get; set; }
    public decimal Qty { get; set; }
    public string? RefNo { get; set; }
    public string? Remarks { get; set; }
}

public class AvailableDto
{
    public int ItemId { get; set; }
    public decimal AvailableQty { get; set; }
}
