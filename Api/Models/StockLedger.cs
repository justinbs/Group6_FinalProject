using System;

namespace Api.Models;

public enum MovementType { In = 1, Out = 2 }

public class StockLedger
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public MovementType Type { get; set; }
    public string? Note { get; set; }
    public DateTime Timestamp { get; set; }

    public Item? Item { get; set; }
}
