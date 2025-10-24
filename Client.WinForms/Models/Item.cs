namespace Client.WinForms.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
        public string? Brand { get; set; }
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
    }
}
