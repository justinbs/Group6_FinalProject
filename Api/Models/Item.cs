namespace Api.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
        public string? Brand { get; set; }
        public decimal UnitPrice { get; set; }

        // Added for inventory
        public int Quantity { get; set; } = 0;

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
