using System;

namespace Client.WinForms.Models
{
    public enum MovementType { Purchase = 1, Sale = 2, Adjustment = 3 }

    public class StockMovement
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public MovementType Type { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string? Remarks { get; set; }
    }
}
