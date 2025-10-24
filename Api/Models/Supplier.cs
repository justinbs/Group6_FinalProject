namespace Api.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Contact { get; set; }

        // Navigation
        public List<Item> Items { get; set; } = new();
    }
}
