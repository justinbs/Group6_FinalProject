namespace Api.Models
{
    public enum MovementType { Purchase = 1, Sale = 2, Adjustment = 3 }

    public class StockMovement
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        public MovementType Type { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Remarks { get; set; }
    }
}
